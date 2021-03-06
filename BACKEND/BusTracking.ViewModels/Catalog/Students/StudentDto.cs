﻿using System;
using System.Collections.Generic;
using System.Text;

namespace BusTracking.ViewModels.Catalog.Students
{
    public class StudentDto
    {
        public int Id { get; set; }
        public int BusId { get; set; }
        public string BusName { get; set; }
        public Guid MonitorId { get; set; }
        public string MonitorName { get; set; }
        public string PhoneMonitor { get; set; }
        public Guid ParentId { get; set; }
        public string ParentName { get; set; }
        public string PhoneParent { get; set; }
        public Guid TeacherId { get; set; }
        public string TeacherName { get; set; }
        public string PhoneTeacher { get; set; }
        public string ClassOfStudent { get; set; }
        public int StopPickId { get; set; }
        public int StopDropId { get; set; }
        public string StopPickName { get; set; }
        public string StopDropName { get; set; }
        public TimeSpan StopPickTime { get; set; }
        public TimeSpan StopDropTime { get; set; }
        public string StopPickAddress { get; set; }
        public string StopDropAddress { get; set; }
        public string Name { get; set; }
        public DateTime Dob { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public int Status { get; set; }
    }
}
