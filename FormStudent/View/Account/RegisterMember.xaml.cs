﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Runtime.InteropServices.WindowsRuntime;
using System.ServiceModel.Channels;
using System.Text;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Media.Capture;
using Windows.Storage;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;
using FormStudent.Entity;
using Newtonsoft.Json;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace FormStudent.View
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class RegisterMember : Page
    {
        private StorageFile photo;
        Member currenMember = new Member();
        

        public static string currenUploadUrl;

        public RegisterMember()
        {
            this.InitializeComponent();
        }

        private void firstName_keyDown(object sender, KeyRoutedEventArgs e)
        {
            if (this.FirtName.Text.Length < 5)
            {
                this.firstName.Text = "Thieu";
                this.firstName.Visibility = Visibility.Visible;
            }
            else
            {
                this.firstName.Visibility = Visibility.Collapsed;
            }
        }

        private async void do_Submit(object sender, RoutedEventArgs e)
        {

            this.currenMember.firstName = this.FirtName.Text;
            this.currenMember.lastName = this.LastName.Text;
            this.currenMember.avatar = this.AvatarURL.Text;
            this.currenMember.address = this.Address.Text;
            this.currenMember.introduction = this.Introduction.Text;
            this.currenMember.phone = this.Phone.Text;
            this.currenMember.email = this.Email.Text;
            this.currenMember.password = this.Password.Password;

            string jsonMember = JsonConvert.SerializeObject(this.currenMember);

            HttpClient httpClient = new HttpClient();
            var content = new StringContent(jsonMember, Encoding.UTF8, "application/json");
            var response = httpClient.PostAsync(Services.ServiceURL.API_URL_REGISTER, content);
            if (response.Result.StatusCode == HttpStatusCode.Created)
            {
                this.Text_Success.Text = "Create Account Success";
                this.Text_Success.Visibility = Visibility.Visible;
            }
            else
            {
                var contents = await response.Result.Content.ReadAsStringAsync();
                ErrorRespone errorRespone = JsonConvert.DeserializeObject<ErrorRespone>(contents);
                Debug.WriteLine(errorRespone.status);
                Debug.WriteLine(errorRespone.message);

                if (errorRespone.error.Count > 0)
                {
                    foreach (var key in errorRespone.error.Keys)
                    {
                        var objectByKey = this.FindName(key);
                        var value = errorRespone.error[key];
                        if (objectByKey != null)
                        {
                            TextBlock textBlock = objectByKey as TextBlock;
                            textBlock.Text = "* " + value;
                            textBlock.Visibility = Visibility.Visible;
                        }
                    }
                }
                
                Debug.WriteLine(contents);
            }

        }

        private void do_Reset(object sender, RoutedEventArgs e)
        {

        }

        private void RadioButton_Checked(object sender, RoutedEventArgs e)
        {
            RadioButton radio = sender as RadioButton;
            this.currenMember.gender = Int32.Parse(radio.Tag.ToString());
        }

        private void Birthday_cheched(CalendarDatePicker sender, CalendarDatePickerDateChangedEventArgs args)
        {
            this.currenMember.birthday = sender.Date.Value.DateTime.ToString("yyyy-MM-dd");
        }

        private async void chose_img(object sender, RoutedEventArgs e)
        {
            CameraCaptureUI captureUI = new CameraCaptureUI();
            captureUI.PhotoSettings.Format = CameraCaptureUIPhotoFormat.Jpeg;
            captureUI.PhotoSettings.CroppedSizeInPixels = new Size(200, 200);

            this.photo = await captureUI.CaptureFileAsync(CameraCaptureUIMode.Photo);

            if (photo == null)
            {
                // User cancelled photo capture
                return;
            }
            HttpClient httpClient = new HttpClient();
            currenUploadUrl = await httpClient.GetStringAsync(Services.ServiceURL.API_UPLOAD_IMG);
            Debug.WriteLine("Upload URL :" + currenUploadUrl);
            HttpUploadFile(currenUploadUrl, "myFile", "image/png");

        }

        public async void HttpUploadFile(string url, string paramName, string contentType)
        {
            string boundary = "---------------------------" + DateTime.Now.Ticks.ToString("x");
            byte[] boundarybytes = System.Text.Encoding.ASCII.GetBytes("\r\n--" + boundary + "\r\n");

            HttpWebRequest wr = (HttpWebRequest)WebRequest.Create(url);
            wr.ContentType = "multipart/form-data; boundary=" + boundary;
            wr.Method = "POST";

            Stream rs = await wr.GetRequestStreamAsync();
            rs.Write(boundarybytes, 0, boundarybytes.Length);

            string header = string.Format("Content-Disposition: form-data; name=\"{0}\"; filename=\"{1}\"\r\nContent-Type: {2}\r\n\r\n", paramName, "path_file", contentType);
            byte[] headerbytes = System.Text.Encoding.UTF8.GetBytes(header);
            rs.Write(headerbytes, 0, headerbytes.Length);

            // write file.
            Stream fileStream = await this.photo.OpenStreamForReadAsync();
            byte[] buffer = new byte[4096];
            int bytesRead = 0;
            while ((bytesRead = fileStream.Read(buffer, 0, buffer.Length)) != 0)
            {
                rs.Write(buffer, 0, bytesRead);
            }

            byte[] trailer = System.Text.Encoding.ASCII.GetBytes("\r\n--" + boundary + "--\r\n");
            rs.Write(trailer, 0, trailer.Length);

            WebResponse wresp = null;
            try
            {
                wresp = await wr.GetResponseAsync();
                Stream stream2 = wresp.GetResponseStream();
                StreamReader reader2 = new StreamReader(stream2);
                //Debug.WriteLine(string.Format("File uploaded, server response is: @{0}@", reader2.ReadToEnd()));
                //string imgUrl = reader2.ReadToEnd();
                //Uri u = new Uri(reader2.ReadToEnd(), UriKind.Absolute);
                //Debug.WriteLine(u.AbsoluteUri);
                //ImageUrl.Text = u.AbsoluteUri;
                //MyAvatar.Source = new BitmapImage(u);
                //Debug.WriteLine(reader2.ReadToEnd());
                string imageUrl = reader2.ReadToEnd();
                Avatar.Source = new BitmapImage(new Uri(imageUrl, UriKind.Absolute));
                AvatarURL.Text = imageUrl;
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Error uploading file", ex.StackTrace);
                Debug.WriteLine("Error uploading file", ex.InnerException);
                if (wresp != null)
                {
                    wresp = null;
                }
            }
            finally
            {
                wr = null;
            }
        }

    }

}
