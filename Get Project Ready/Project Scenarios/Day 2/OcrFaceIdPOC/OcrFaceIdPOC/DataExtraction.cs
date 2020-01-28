using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace OcrFaceIdPOC
{
    public class DataExtraction
    {

        public string pincode = "";
        public string adrline = "";
        public string address = "";
        public string mobile = "";
        public string email = "";
        public string website = "";
        public string userdetails = "";
        public string name = "";

        public string compweb = "";
        public string compmail = "";
        public string company = "";

        public void BusinessCardReader(List<string> OCRResponse)
        {

            

            //Fetching Card Details
            for (int i = 0; i < OCRResponse.Count; i++)
            {
                var result = OCRResponse[i];

                //Fetching Address
                if (Regex.IsMatch(result, @" \d{3} \d{3}") && !Regex.IsMatch(result, @"[+]"))
                {
                    pincode = result;
                }
                if (Regex.IsMatch(result, @"No.\d*") || Regex.IsMatch(result, @"No:\d*") || (Regex.IsMatch(result, @",") && !Regex.IsMatch(result, @"[+]") && !Regex.IsMatch(result, @" \d{3} \d{3}") && !Regex.IsMatch(result, @"044") && !Regex.IsMatch(result, @"[@]")) || Regex.IsMatch(result, @"#") || (Regex.IsMatch(result, @"[.]") && !Regex.IsMatch(result, @"[@]") && !Regex.IsMatch(result, @"\b[.]com\b") && !Regex.IsMatch(result, @"[+]") && !Regex.IsMatch(result, @" \d{3} \d{3}") && !Regex.IsMatch(result, @"\bwww\b") && !Regex.IsMatch(result, @"^\w{1}\.\w+ \w+$") && !Regex.IsMatch(result, @"^\w{1}\. \w+ \w+$") && !Regex.IsMatch(result, @"^[A-Z ]+$") && !Regex.IsMatch(result, @"^[A-Z ().]+$") && !Regex.IsMatch(result, @"\.\.")))
                {
                    adrline += result + " ";
                }

                //Fetching Website
                if (Regex.IsMatch(result, @"^www\."))
                {
                    website = result;
                }

                //Fetching Contact Number
                if (Regex.IsMatch(result, @"^(\+91[\-\s]?)?[0]?(91)?[789]\d{9}$") || Regex.IsMatch(result, @"Ph:") || Regex.IsMatch(result, @"Ph :") || Regex.IsMatch(result, @"Phone :") || Regex.IsMatch(result, @"Tel :") || Regex.IsMatch(result, @"\+91 \d{4} \d{6}$") || Regex.IsMatch(result, @"Cell:") || Regex.IsMatch(result, @"\+91 \d{4} \d{3} \d{3}$") || Regex.IsMatch(result, @"\+91 \d{5} \d{5}$") || Regex.IsMatch(result, @"^\d{5} \d{5}$") || Regex.IsMatch(result, @"M \+ 91") || Regex.IsMatch(result, @"W \+ 91") || Regex.IsMatch(result, @"Mobile : \+91") || Regex.IsMatch(result, @"^\( \d{10}$") || Regex.IsMatch(result, @"Mobile : \d{10}$") || Regex.IsMatch(result, @"Cell : \d{10}$") || Regex.IsMatch(result, @"^\d{7}$"))
                {
                    if (result.StartsWith("Cell : "))
                    {
                        string strtindx4 = result.Substring(7);
                        mobile += result.Substring(7, strtindx4.Length) + ",";
                    }
                    else if (result.StartsWith("Cell:"))
                    {
                        string strtindxx4 = result.Substring(6);
                        mobile += result.Substring(6, strtindxx4.Length) + ",";
                    }
                    else if (result.StartsWith("Ph:"))
                    {
                        string strtindx5 = result.Substring(4);
                        mobile += result.Substring(4, strtindx5.Length) + ",";
                    }
                    else if (result.StartsWith("Ph :"))
                    {
                        string strtindx6 = result.Substring(4);
                        mobile += result.Substring(4, strtindx6.Length) + ",";
                    }
                    else if (result.StartsWith("Phone :"))
                    {
                        string strtindxx6 = result.Substring(7);
                        mobile += result.Substring(7, strtindxx6.Length) + ",";
                    }
                    else if (result.StartsWith("Mobile : "))
                    {
                        string strtindx7 = result.Substring(9);
                        mobile += result.Substring(9, strtindx7.Length) + ",";
                    }
                    else if (result.StartsWith("Tel :"))
                    {
                        string strtindxx7 = result.Substring(6);
                        mobile += result.Substring(6, strtindxx7.Length) + ",";
                    }
                    else
                    {
                        mobile += result + ",";
                    }

                }

                //Fetching Email
                if (Regex.IsMatch(result, @"\b@\b") && ((Regex.IsMatch(result, @"\.in$") && (!Regex.IsMatch(result, @"www\."))) || Regex.IsMatch(result, @"\.com$") && (!Regex.IsMatch(result, @"^www\."))))
                {
                    if (result.StartsWith("Email : "))
                    {
                        string strtindx8 = result.Substring(8);
                        email = result.Substring(8, strtindx8.Length);
                    }
                    else if (result.StartsWith("E-mail : "))
                    {
                        string strtindx9 = result.Substring(9);
                        email = result.Substring(9, strtindx9.Length);
                    }
                    else if (result.StartsWith("Email:"))
                    {
                        string strtindxx9 = result.Substring(7);
                        email = result.Substring(7, strtindxx9.Length);
                    }
                    else
                    {
                        email = result;
                    }
                }

                //Fetching Name
                if (Regex.IsMatch(result, @"^\w{1}\.\w+ \w+$") || Regex.IsMatch(result, @"^\w{1}\. \w+ \w+$") || Regex.IsMatch(result, @"^\w{1}\. \w+$") || Regex.IsMatch(result, @"^\w{2}\. \w+$"))
                {
                    name = result;
                }

                //Fetching Company Name
                if ((Regex.IsMatch(result, @"^[A-Z ]+$") || Regex.IsMatch(result, @"^[A-Z ().]+$") || Regex.IsMatch(result, @"Pvt\. Ltd\.$")) && !Regex.IsMatch(result, @"^MI DUAL CAMERA$") && !Regex.IsMatch(result, @"^REDMI NOTE 5 PRO$") && !Regex.IsMatch(result, @"^O$") && !Regex.IsMatch(result, @"^OO$") && !Regex.IsMatch(result, @"^\.$"))
                {
                    company = result;
                }

            }


            if (mobile.EndsWith(","))
            {
                mobile = mobile.Substring(0, mobile.Length - 1);
            }

            //Company Prerequisites
            if (website.Length != 0)
            {
                string substr1 = website.Substring(4);
                int endindx1 = substr1.IndexOf('.');
                compweb = website.Substring(4, endindx1);
            }
            else if ((website.Length == 0) && (email.Length != 0))
            {
                int strtindx2 = email.IndexOf('@');
                string substr2 = email.Substring(strtindx2);
                int endindx2 = substr2.IndexOf('.');
                compmail = email.Substring(strtindx2 + 1, endindx2 - 1);

            }

            //Company
            if (company.Length != 0 && !Regex.IsMatch(company, @"Pvt\. Ltd\.$"))
            {
                if (compweb.Length != 0)
                {
                    company = compweb;
                }
                else if (compmail.Length != 0)
                {
                    company = compmail;
                }
            }
            else if (company.Length == 0 && !Regex.IsMatch(company, @"Pvt\. Ltd\.$"))
            {
                if (compweb.Length != 0)
                {
                    company = compweb;
                }
                else if (compmail.Length != 0)
                {
                    company = compmail;
                }
            }


            //Name
            if (name.Length != 0)
            {
                if (email.Length != 0)
                {
                    int endindx3 = email.IndexOf('@');
                    string temp1 = email.Substring(0, endindx3);

                    for (int j = 0; j < OCRResponse.Count; j++)
                    {
                        var result1 = OCRResponse[j];

                        if (result1.StartsWith(temp1, StringComparison.OrdinalIgnoreCase) && !result1.Equals(email, StringComparison.OrdinalIgnoreCase))
                        {
                            name = result1;
                        }

                    }
                }
            }
            else if (name.Length == 0)
            {
                if (email.Length != 0)
                {
                    int endindx9 = email.IndexOf('@');
                    string temp9 = email.Substring(0, endindx9);

                    for (int x = 0; x < OCRResponse.Count; x++)
                    {
                        var result9 = OCRResponse[x];

                        if (result9.StartsWith(temp9, StringComparison.OrdinalIgnoreCase) && !result9.Equals(email, StringComparison.OrdinalIgnoreCase))
                        {
                            name = result9;
                        }
                        else if (temp9.Contains("."))
                        {
                            string tempp9 = temp9.Replace('.', ' ');

                            if (result9.StartsWith(tempp9, StringComparison.OrdinalIgnoreCase) && !result9.Equals(email, StringComparison.OrdinalIgnoreCase))
                            {
                                name = result9;
                            }
                        }
                    }
                }
            }

            if (mobile.Length != 0)
            {
                mobile = mobile.Substring(0, mobile.Length) + "";
            }


            address = string.Concat(adrline, pincode);
        }
    }
}