﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FormStudent.Entity
{
    class Song
    {
        private long _id;
        private string _name;
        private string _author;
        private string _thumbnail;
        private string _link;
        private string _description;
        private string _singer;
        private long _memberId;
        private string _createdAt;
        private string _updatedAt;
        private int _status;

        public long id { get => _id; set => _id = value; }
        public string name { get => _name; set => _name = value; }
        public string author { get => _author; set => _author = value; }
        public string thumbnail { get => _thumbnail; set => _thumbnail = value; }
        public string link { get => _link; set => _link = value; }
        public string description { get => _description; set => _description = value; }
        public string singer { get => _singer; set => _singer = value; }
        public long memberId { get => _memberId; set => _memberId = value; }
        public string createdAt { get => _createdAt; set => _createdAt = value; }
        public string updatedAt { get => _updatedAt; set => _updatedAt = value; }
        public int status { get => _status; set => _status = value; }
    }
}
