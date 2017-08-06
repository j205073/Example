using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Reflection.Models
{
    public class Model1
    {
        int m_id = 0;
        string m_name = "";
        public int ID { get { return this.m_id; } set { this.m_id = value; } }
        public string Name { get { return this.m_name; } set { this.m_name = value; } }

    }
}