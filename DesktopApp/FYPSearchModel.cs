using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DesktopApp
{
    public class FYPSearchModel
    {
        public String supervisor { get; set; }
        public String member1 { get; set; }
        public String member2 { get; set; }
        public String member3 { get; set; }
        public String title { get; set; }
        public String description { get; set; }

        public FYPSearchModel(String supervisor, String member1, String member2, String member3, String title, String description)
        {
            this.supervisor = supervisor;
            this.member1 = member1;
            this.member2 = member2;
            this.member3 = member3;
            this.title = title;
            this.description = description;
        }

        private FYPSearchModel() { }

        public static FYPSearchModel generate(Dictionary<string, object> pairs)
        {
            FYPSearchModel model = new FYPSearchModel();
            model.supervisor = (string) pairs["supervisor"];
            model.member1 = (string) pairs["member1"];
            model.member2 = (string) pairs["member2"];
            model.member3 = (string) pairs["member3"];
            model.title = (string) pairs["title"];
            model.description = (string) pairs["description"];
            return model;
        }

    }
}