using NUnit.Framework.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModulF.Tage
{
    internal class _06 : BaseClass
    {
        internal override void ResolveTasks() { 
        
            List<Person2> list = new List<Person2>();
            list.Add(new Person2("name1"));
            list.Add(new Person2Natural("namePres", "namePos", Gender.female));

            foreach (Person2 person2 in list)
            {
                Console.WriteLine("1");
                Console.WriteLine(person2.GetAdress());
                Console.WriteLine("2");
                Console.WriteLine(person2.GetReference());
                if(person2 is Person2Natural)
                {
                    Console.WriteLine("3");
                    var p2 = (Person2Natural)person2;
                    Console.WriteLine(p2.namePre);
                    
                        
                }
            }

            Console.ReadKey(intercept:true);
        }
    }
    internal class Person2
    {
        internal string name { get; set; }
        internal string postalCode { get; set; }
        internal string city { get; set; }
        internal string streetNumber { get; set; }
        internal string GetAdress()
        {
            return streetNumber + "\n" + city + "\n" + postalCode + "\n";
        }

        internal virtual string GetReference() {
            return name;
        }

        internal Person2(string name, string postalCode = "postal Code", string city = "city", string streetNumber = "street num")
        {
            this.name = name;
            this.postalCode = postalCode;
            this.city = city;
            this.streetNumber = streetNumber;
        }
    }

    internal class Person2Natural : Person2
    {
        internal string name { get; set; }
        internal string namePre { get; set; }
        internal string namePos { get; set; }
        internal Gender gender { get; set; }

        internal override string GetReference()
        {
            string answer = "";
            switch (this.gender)
            {
                case Gender.unknown:
                    answer += "Sehr geehrtes Unknown ";
                    break;
                case Gender.male:
                    answer += "Sehr geehrter Herr ";
                    break;
                case Gender.female:
                    answer += "Sehr geehrte Frau ";
                    break;
                case Gender.futanari:
                    answer += "Sehr geehrte Futa ";
                    break;
            }

            return answer + base.GetReference();
        }

        internal Person2Natural(string namePre, string namePos, Gender gender) : base(namePre+" "+namePos)
        {
            this.namePre = namePre;
            this.namePos = namePos;
            this.gender = gender;
        }
    }


    internal enum Gender
    {
        unknown = 0,
        male = 1,
        female = 2,
        futanari = 3
    }


    internal abstract class Person3
    {
        public string name { get; set; }
        public string namePre { get; set; }
        abstract internal void DoSomething();
        internal void DoNothing() {
            string lol = "2323";
        }

    }
    internal class Person3NotAbstract : Person3
    {
        internal override void DoSomething()
        {
            throw new NotImplementedException();
        }
    }

    internal interface IPerson4
    {
        string name { get; set; }
        string id { get; set; }
        string GetID();
    }

    internal class Person4 : Person3, IPerson4
    {
        public string id { get; set; }

        public string GetID()
        {
            throw new NotImplementedException();
        }

        internal override void DoSomething()
        {
            throw new NotImplementedException();
        }
    }
}
