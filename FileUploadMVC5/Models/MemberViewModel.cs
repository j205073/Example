using System.Collections.Generic;
using WebApplication1.Enums;

namespace WebApplication1.Models
{

    public class ImagesModel
    {
        public string ActionType { get; set; }
        public string ActionName { get; set; }

        List<MemberViewModel> m_memberData = new List<MemberViewModel>();
        public List<MemberViewModel> MemberData { get { return m_memberData; } set { this.m_memberData = value; } }
    }

    public class MemberViewModel
    {
        private ModelInitProperty m_InitProperty = ModelInitProperty.New;

        internal ModelInitProperty InitProperty
        {
            get { return m_InitProperty; }
            set { m_InitProperty = value; }
        }

        public int ID { get; set; }
        public string FileName { get; set; }

        public string FileUrl { get; set; }
    }
}