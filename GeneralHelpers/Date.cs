using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;

namespace AdConta
{
    public class Date
    {
        public Date(DateTime dateT)
        {
            this._Year = dateT.Year;
            this._Month = dateT.Month;
            this._Day = dateT.Day;
        }
        public Date(int year, int month, int day)
        {
            this._Year = year;

            if (month < 1 || month > 12)
                throw new Exception("Invalid month creating Date object");
            this._Month = month;

            if(day < 1 || day > DateTime.DaysInMonth(year, month))
                throw new Exception("Invalid day creating Date object");
            this._Day = day;
        }
        public Date(int month, int day)
        {
            this._Year = DateTime.Today.Year;

            if (month < 1 || month > 12)
                throw new Exception("Invalid month creating Date object");
            this._Month = month;

            if (day < 1 || day > DateTime.DaysInMonth(this._Year, month))
                throw new Exception("Invalid day creating Date object");
            this._Day = day;
        }
        public Date(int day)
        {
            this._Year = DateTime.Today.Year;
            this._Month = DateTime.Today.Month;

            if (day < 1 || day > DateTime.DaysInMonth(this._Year, this._Month))
                throw new Exception("Invalid day creating Date object");
            this._Day = day;
        }
        public Date() { }

        #region fields
        private int _Year;
        private int _Month;
        private int _Day;
        #endregion

        #region properties
        public int Year
        {
            get { return this._Year; }
            set { this._Year = value; }
        }
        public int Month
        {
            get { return this._Month; }
            set { this._Month = value; }
        }
        public int Day
        {
            get { return this._Day; }
            set { this._Day = value; }
        }
        #endregion

        #region helpers
        #endregion

        #region public methods
        public DateTime GetDateTime()
        {
            return new DateTime(this.Year, this.Month, this.Day);
        }
        public override string ToString()
        {
            string paddedDay = this.Day.ToString().PadLeft(2);
            string paddedMonth = this.Month.ToString().PadLeft(2);
            return string.Format("{0}/{1}/{2}", paddedDay, paddedMonth, this.Year);
        }
        public string ToString(string param)
        {
            switch(param)
            {
                case "d":
                    return this.ToString();
                case "D":
                    return string.Format("{0} de {1} del {3}", 
                        this.Day, 
                        CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(this.Month),
                        this.Year);
                default:
                    return this.GetDateTime().ToString();
            }
        }
        #endregion
    }

}
