using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using NewEventLogDLL;

namespace EmailEventLog
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        //setting up the classes
        WPFMessagesClass TheMessagesClass = new WPFMessagesClass();
        EventLogClass TheEventLogClass = new EventLogClass();
        SendEmailClass TheSendEmailClass = new SendEmailClass();

        //getting the data
        FindEventLogEntriesForEmailDataSet TheFindEventLogEntriesForEmailDataSet = new FindEventLogEntriesForEmailDataSet();

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            int intCounter;
            int intNumberOfRecords;
            string strEventID;
            string strEventDate;
            string strLogEntry;
            string strMessage;

            try
            {
                TheFindEventLogEntriesForEmailDataSet = TheEventLogClass.FindEventLogEntriesForEmail();

                intNumberOfRecords = TheFindEventLogEntriesForEmailDataSet.FindEventLogEntriesForEmail.Rows.Count;

                strMessage = "<h1>Event Log Entries For the last 15 Days</h1>";
                strMessage += "<table><tr><th>Event ID </th><th>Event Date</th><th>Log Entry</th></tr>";

                if(intNumberOfRecords > 0)
                {
                    for (intCounter = 0; intCounter < intNumberOfRecords; intCounter++)
                    {
                        strEventID = Convert.ToString(TheFindEventLogEntriesForEmailDataSet.FindEventLogEntriesForEmail[intCounter].EventID);
                        strEventDate = Convert.ToString(TheFindEventLogEntriesForEmailDataSet.FindEventLogEntriesForEmail[intCounter].EventDate);
                        strLogEntry = TheFindEventLogEntriesForEmailDataSet.FindEventLogEntriesForEmail[intCounter].LogEntry;

                        strMessage += "<tr><th>" + strEventID + "</th>";
                        strMessage += "<th>" + strEventDate + "</th>";
                        strMessage += "<th>" + strLogEntry + "</th></tr>";
                    }
                }

                strMessage += "</table>";

                TheSendEmailClass.SendEventLogReport(strMessage);

                Application.Current.Shutdown();
            }
            catch (Exception Ex)
            {
                TheSendEmailClass.SendEventLog(Ex.ToString());
            }

        }
    }
}
