using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CSMedicalRepresentativeScheduleMicroservice.Models
{
    //public class RepSchedule
    //{
    //    public string MedicalRepresentativeName { get; set; }
    //    public string DoctorName { get; set; }
    //    public string MeetingSlot { get; set; }
    //    public DateTime DateOfMeeting { get; set; }
    //    public string DoctorContactNumber { get; set; }

    //}

    public class MedicalRep
    {
        public int MedicalRepId { get; set; }
        public string MedicalRepName { get; set; }
        public string MedicalRepConatactNo { get; set; }

        List<MedicalRep> medicalReps = new List<MedicalRep>();
        public MedicalRep()
        {

        }

        public MedicalRep(int MedicalRepId, string MedicalRepName, string MedicalRepConatactNo)
        {
            this.MedicalRepId = MedicalRepId;
            this.MedicalRepName = MedicalRepName;
            this.MedicalRepConatactNo = MedicalRepConatactNo;
        }

        public List<MedicalRep> getMedicalReps()
        {
            medicalReps.Add(new MedicalRep(101, "john", "9999999999"));
            medicalReps.Add(new MedicalRep(102, "jack", "9999999998"));
            medicalReps.Add(new MedicalRep(103, "anne", "9999999994"));
            return medicalReps;
        }
    }

   

    public class Doctor
    {
        public int DocId { get; set; }
        public string DoctorName { get; set; }
        public string DoctorContactNumber { get; set; }
        public string TreatingAilment { get; set; }


        List<Doctor> doctors = new List<Doctor>();
        public Doctor()
        {

        }

        public Doctor(int DocId, string DoctorName, string DoctorContactNumber, string TreatingAilment)
        {
            this.DocId = DocId;
            this.DoctorName = DoctorName;
            this.DoctorContactNumber = DoctorContactNumber;
            this.TreatingAilment = TreatingAilment;
        }

        public List<Doctor> getDoctors()
        {
            doctors.Add(new Doctor(11, "john", "9999999999","General"));
            doctors.Add(new Doctor(12, "Eras", "9999999998", "Gynaecology"));
            doctors.Add(new Doctor(13, "Penn", "9999999997", "Orthopaedics"));
            doctors.Add(new Doctor(14, "Fruity", "9999999996", "General"));
            doctors.Add(new Doctor(15, "Gingy", "9999999995", "Gynaecology"));
            doctors.Add(new Doctor(16, "Penny", "9999999994", "Orthopaedics"));
            return doctors;
        }

    }
}

