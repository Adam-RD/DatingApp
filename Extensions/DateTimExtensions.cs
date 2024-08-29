using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Extensions
{
    public static class DateTimExtensions
    {
        public static int CalculateAge(this DateOnly dbo){

             var today = DateOnly.FromDateTime(DateTime.Now);

             var age = today.Year - dbo.Year;

             if (dbo > today.AddYears(-age)) age --;

             return age;

        }
        
       
        
    }
}