using CSPharmacyMedicineSupplyManagement.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Newtonsoft.Json;
using CSMedicalRepresentativeScheduleMicroservice.Models;
using Microsoft.AspNetCore.Authorization;

namespace CSMedicalRepresentativeScheduleMicroservice.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class MedicalRepresentativeScheduleController : ControllerBase
    {
        private readonly PharmacyMedicineSupplyManagementContext db;

        public MedicalRepresentativeScheduleController(PharmacyMedicineSupplyManagementContext _db)
        {
            db = _db;
        }


        [HttpGet]
        [Route("RepScheduler")]
        public async Task<List<RepSchedule>> RepSchedule(DateTime scheduleStartDate)
        {
            List<RepSchedule> returnRepScheduleList = new List<RepSchedule>();
            List<MedicalRep> medicalRepList = new List<MedicalRep>();
            List<Doctor> doctorList = new List<Doctor>();
            MedicalRep medicalRep = new MedicalRep();
            Doctor doctor = new Doctor();

            try
            {
                medicalRepList = medicalRep.getMedicalReps();
                doctorList = doctor.getDoctors();


                string Baseurl = "https://localhost:44318/";
                List<MedicineStock> medicineStockList = new List<MedicineStock>();

                using (var client = new HttpClient())
                {
                    //Passing service base url  
                    client.BaseAddress = new Uri(Baseurl);

                    client.DefaultRequestHeaders.Clear();
                    //Define request data format  
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    //Sending request to find web api REST service resource GetAllEmployees using HttpClient  
                    HttpResponseMessage Res = await client.GetAsync("api/MedicineStock");

                    //Checking the response is successful or not which is sent using HttpClient  
                    if (Res.IsSuccessStatusCode)
                    {
                        //Storing the response details recieved from web api   
                        var ProductResponse = Res.Content.ReadAsStringAsync().Result;

                        //Deserializing the response recieved from web api and storing into the Employee list  
                        medicineStockList = JsonConvert.DeserializeObject<List<MedicineStock>>(ProductResponse);

                        int startDate = 0;
                        int index = 0;
                        foreach (var item in doctorList)
                        {
                            RepSchedule repSchedule = new RepSchedule();
                            repSchedule.Doctorname = item.DoctorName;
                            repSchedule.DoctorContactNumber = item.DoctorContactNumber;
                            repSchedule.MeetingSlot = "1 PM - 2 PM";
                            if (medicalRepList.Count == index)
                            {
                                index = 0;
                            }

                            repSchedule.Name = medicalRepList[index].MedicalRepName;

                            repSchedule.TreatingAilment = item.TreatingAilment;
                            foreach (var medicineItem in medicineStockList)
                            {
                                if (medicineItem.TargetAilment == item.TreatingAilment)
                                {
                                    repSchedule.Medicine = medicineItem.MedicineName;
                                }
                            }

                            if (scheduleStartDate.AddDays(startDate).DayOfWeek == DayOfWeek.Sunday)
                            {
                                startDate++;
                            }

                            repSchedule.DateOfMeeting = scheduleStartDate.AddDays(startDate);
                            returnRepScheduleList.Add(repSchedule);
                            startDate++;
                            index++;
                        }

                        return returnRepScheduleList;
                    }
                    else
                    {
                        return null;
                    }
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}
