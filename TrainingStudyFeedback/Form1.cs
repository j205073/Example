using System;
using System.Windows.Forms;
using TrainingStudyFeedback.Repositoies;

namespace TrainingStudyFeedback
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        /// <summary>
        /// TrainingStudyOpinionFeedback 學員意見調查表，受訓新得報告 於上課第7日未填寫通知功能
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Form1_Load(object sender, EventArgs e)
        {
            UnityRepository unityRepository = new UnityRepository();
            unityRepository.SandMailHandlerFromStudyOpinion();
            unityRepository.SandMailHandlerFromFeedBack();
        }
    }
}