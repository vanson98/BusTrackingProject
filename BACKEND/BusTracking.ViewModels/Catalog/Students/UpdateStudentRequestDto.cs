﻿using System;
using System.Collections.Generic;
using System.Text;

namespace BusTracking.ViewModels.Catalog.Students
{
    public class UpdateStudentRequestDto
    {
        public int Id { get; set; }
        public int BusId { get; set; }
        public Guid ParentId { get; set; }
        public int StopPickId { get; set; }
        public int StopDropId { get; set; }
        public string Name { get; set; }
        public DateTime Dob { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string ClassOfStudent { get; set; }
        public Guid TeacherId { get; set; }
    }
}
