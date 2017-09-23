using Core;
using System;

namespace SampleApplication
{
    public class Contact : ModelBase
    {
        private string _email;
        private string _name;
        private DateTime? _nextAppointmentDate;
        private string _notes;
        private string _phone;
        private string _pictureName;

        public string Email
        {
            get { return _email; }
            set { SetProperty(ref _email, value); }
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

        public string Notes
        {
            get { return _notes; }
            set { SetProperty(ref _notes, value); }
        }

        public string Phone
        {
            get { return _phone; }
            set { SetProperty(ref _phone, value); }
        }

        public string PictureName
        {
            get { return _pictureName; }
            set { SetProperty(ref _pictureName, value); }
        }
    }
}