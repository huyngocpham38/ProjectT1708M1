using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FormStudent.Entity
{
    class Member
    {
        private long _id;
        private String _firtsName;
        private String _lastName;
        private String _avatar;
        private String _phone;
        private String _address;
        private String _introduction;
        private int _gender;
        private String _birthday;

        private String _email;
        private String _password;
        private String _salt;

        private String _createdAt;
        private String _updatedAt;
        private int _status;

        public long id { get => _id; set => _id = value; }
        public string firstName { get => _firtsName; set => _firtsName = value; }
        public string lastName { get => _lastName; set => _lastName = value; }
        public string avatar { get => _avatar; set => _avatar = value; }
        public string phone { get => _phone; set => _phone = value; }
        public string address { get => _address; set => _address = value; }
        public string introduction { get => _introduction; set => _introduction = value; }
        public int gender { get => _gender; set => _gender = value; }
        public string birthday { get => _birthday; set => _birthday = value; }
        public string email { get => _email; set => _email = value; }
        public string password { get => _password; set => _password = value; }
        public string salt { get => _salt; set => _salt = value; }
        public string createdAt { get => _createdAt; set => _createdAt = value; }
        public string updatedAt { get => _updatedAt; set => _updatedAt = value; }
        public int status { get => _status; set => _status = value; }
    }
}
