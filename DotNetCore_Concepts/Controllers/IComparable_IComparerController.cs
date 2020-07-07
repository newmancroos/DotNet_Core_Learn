using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DotNetCore_Concepts.Controllers
{
    [Route("compare")]
    public class IComparable_IComparerController : ControllerBase
    {

        public IActionResult Index()
        {
            return Ok(smartPhones);
        }

        [Route("comparable")]
        public IActionResult Comparable()
        {
  
            smartPhones.Sort();
            return Ok(smartPhones);
        }

        [Route("comparer")]
        public IActionResult Comparer()
        {
            ThirdPartySort thirdPartySort = new ThirdPartySort();
            ThridpartyPhones.Sort(thirdPartySort);
            smartPhones.Sort();
            return Ok(ThridpartyPhones);
        }

        List<SmartPhone> smartPhones = new List<SmartPhone>()
            {
                new SmartPhone()
                {
                    Name = "One Plus 8",
                    IsFlagship = true,
                    OS = "Android 10",
                    Price = 55000
                },
                new SmartPhone()
                {
                    Name = "IPhone 11",
                    IsFlagship = true,
                    OS = "IOS 11",
                    Price = 75000
                },
                new SmartPhone()
                {
                    Name = "Samsung Note 10",
                    IsFlagship = true,
                    OS = "Android 10",
                    Price = 110000
                },
                new SmartPhone()
                {
                    Name = "Samsung S20 Ultra",
                    IsFlagship = true,
                    OS = "Android 10",
                    Price = 130000
                },
                new SmartPhone()
                {
                    Name = "IPhone 11 Pro",
                    IsFlagship = true,
                    OS = "IOS 11",
                    Price = 130
                }
            };

        List<ThirdParty> ThridpartyPhones = new List<ThirdParty>()
            {
                new ThirdParty()
                {
                    Name = "One Plus 8",
                    IsFlagship = true,
                    OS = "Android 10",
                    Price = 55000
                },
                new ThirdParty()
                {
                    Name = "IPhone 11",
                    IsFlagship = true,
                    OS = "IOS 11",
                    Price = 75000
                },
                new ThirdParty()
                {
                    Name = "Samsung Note 10",
                    IsFlagship = true,
                    OS = "Android 10",
                    Price = 110000
                },
                new ThirdParty()
                {
                    Name = "Samsung S20 Ultra",
                    IsFlagship = true,
                    OS = "Android 10",
                    Price = 130000
                },
                new ThirdParty()
                {
                    Name = "IPhone 11 Pro",
                    IsFlagship = true,
                    OS = "IOS 11",
                    Price = 130
                }
            };
    }

    class SmartPhone : IComparable
    {
        public string Name { get; set; }
        public double Price { get; set; }
        public string OS { get; set; }
        public bool IsFlagship { get; set; }

        public override string ToString()
        {
            return "Name: " + Name + "||Price: " + Price + "||OS :" + OS + "||Is Flagship : " + IsFlagship; 
        }
        public int CompareTo(object obj)
        {
            if(obj == null) return 1;
            SmartPhone nextSmartPhone = obj as SmartPhone;
            if (nextSmartPhone != null)
            {
                return this.Price.CompareTo(nextSmartPhone.Price);
            }
            else
            {
                throw new ArgumentException("Object doesn't support have a proper price");
            }
        }
    }

    class ThirdParty
    {
        public string Name { get; set; }
        public double Price { get; set; }
        public string OS { get; set; }
        public bool IsFlagship { get; set; }
    }

    class ThirdPartySort : IComparer<ThirdParty>
    {
        public int Compare([AllowNull] ThirdParty x, [AllowNull] ThirdParty y)
        {
            ThirdParty x1 = x as ThirdParty;
            ThirdParty y1 = y as ThirdParty;
            return x1.Price.CompareTo(y1.Price);
        }
    }
}