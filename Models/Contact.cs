using Core;
using System;

namespace SampleApplication
{
    public class Contact : ModelBase
    {
        private string _description;

        private string _name;
        private DateTime? _nextAppointmentDate;
        private byte[] _picture;

        public string Description
        {
            get { return _description; }
            set { SetProperty(ref _description, value); }
        }

        public string Name
        {
            get { return _name; }
            set { SetProperty(ref _name, value); }
        }

        public DateTime? NextAppointmentDate
        {
            get { return _nextAppointmentDate; }
            set { SetProperty(ref _nextAppointmentDate, value); }
        }

        public byte[] Picture
        {
            get { return _picture; }
            set { SetProperty(ref _picture, value); }
        }
    }
}