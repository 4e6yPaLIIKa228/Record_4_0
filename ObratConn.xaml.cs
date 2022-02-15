using System;
using System.Net;
using System.Net.Mail;
using System.Net.Mime;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows;

namespace Record
{
    /// <summary>
    /// Логика взаимодействия для ObratConn.xaml
    /// </summary>
    public partial class ObratConn : Window
    {
        public ObratConn()
        {
            InitializeComponent();
        }

        private void BtMes_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                SmtpClient Smtp = new SmtpClient("smtp.mail.ru");   //OAoTiaauI)65 pass
                Smtp.UseDefaultCredentials = true;
                Smtp.EnableSsl = true;
                Smtp.Credentials = new NetworkCredential("fujiwara_1990@mail.ru", "e2JVTUjQGi7Tdt4Y5ZzS");
               

                //Формирование письма
                MailMessage Message = new MailMessage();
                Message.From = new MailAddress("fujiwara_1990@mail.ru");
                Message.To.Add(new MailAddress("fujiwara_1990@mail.ru"));
                Message.Subject = "Обратная свзяь";
                Message.Body = mess.Text;

                Smtp.Send(Message);//отправка
                MessageBox.Show("Сообщение отправлено");
            }
            catch (SmtpException ex)
            {
                throw ex;
            }
        }
    }
    
}
