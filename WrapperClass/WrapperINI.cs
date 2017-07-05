using System;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Linq;

namespace WrapperUnion
{

    /// <summary>
    /// This exception is thrown whenever the specified section does not exist.
    /// </summary>
    public class SectionDoesNotExistException : Exception
    {

        private string _msg = string.Empty;
        private string _section = string.Empty;

        public override string Message
        {
            get { return string.Format("The section \"{0}\" does not exist!", _section); }
        }

        public SectionDoesNotExistException(string section)
        {
            _section = section;
        }

    }

    /// <summary>
    /// This exception is thrown whenever the specified field inside a section does not exist.
    /// </summary>
    public class FieldDoesNotExistException : Exception
    {

        private string _msg = string.Empty;
        private string _section = string.Empty;
        private string _field = string.Empty;

        public override string Message
        {
            get { return string.Format("The field \"{0}\" does not exist in the section \"{1}\"!", _field, _section); }
        }

        public FieldDoesNotExistException(string section, string field)
        {
            _section = section;
            _field = field;
        }

    }


    /// <summary>
    /// This structure contains the INI data read from the file.
    /// </summary>
    public struct Item
    {

        /// <summary>
        /// The field name of this item.
        /// </summary>
        public string Field { get; set; }

        /// <summary>
        /// The value of the object.
        /// 
        /// The type of value will either be:
        /// 1) String
        /// 2) Boolean
        /// 3) Decimal
        /// - Decimal field is implemented for both of int and double.
        /// - if required return value (GetDoubleField) is float? --> casting is recommended.
        /// </summary>
        public object Value { get; set; }
        public object Comment { get;  set; }

    }

    /// <summary>
    /// An easy to use, managed class to create, read, write, and modify INI files.
    /// </summary>
    public class WrapperINI
    {

        string strMainPath = string.Empty;
        #region Properties

        /// <summary>
        /// Internal use, used for making sure sections and their respective items are kept organized.
        /// </summary>
        private Dictionary<string, List<Item>> Items { get; set; }

        /// <summary>
        /// Get the different sections in the INI file.
        /// </summary>
        public IEnumerable<string> Sections { get { return Items.Keys.ToList(); } }

        #endregion

        #region Initialization and Getter

        /// <summary>
        /// Creates a new INI class.
        /// </summary>
        public WrapperINI()
        {
            Items = new Dictionary<string, List<Item>>();
            
        }

        /// <summary>
        /// A public accessor to retrieve items based on their section
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public List<Item> this[string name]
        {
            get
            {
                if (!SectionExists(name))
                    Add(name);
                return Items[name];
            }
            set
            {
                if (!SectionExists(name))
                    Add(name);
                Items[name] = value;
            }
        }

        #endregion
        public void Clear()
        {
            Items.Clear();
        }
        #region Add & Remove

        /// <summary>
        /// Adds a new section to the INI file.
        /// </summary>
        /// <param name="section">The name of the section to add.</param>
        public void Add(string section)
        {
            if (!SectionExists(section))
                Items.Add(section, new List<Item>());
        }

        /// <summary>
        /// Adds a new field to the INI file.
        /// </summary>
        /// <param name="section">The section to add the field under.</param>
        /// <param name="field">The name of the field.</param>
        /// <param name="value">The value to add. The value must be either a number, a boolean, or a string.</param>
        public void Add(string section, string field, object value, string comment)
        {
            if (!SectionExists(section) || (FieldExists(section, field))) return;
            this[section].Add(new Item { Field = field, Value = value, Comment = comment });
        }

         /// <summary>
        /// Removes a section from the INI file.
        /// </summary>
        /// <param name="section">The name of the section to remove.</param>
        public void Remove(string section)
        {
            if (!SectionExists(section)) return;
            Items.Remove(section);
        }

        /// <summary>
        /// Removes a field from a specified section in the INI file.
        /// </summary>
        /// <param name="section">The name of the section to look under.</param>
        /// <param name="field">The name of the field to remove.</param>
        public void Remove(string section, string field)
        {
            if (!SectionExists(section) || !FieldExists(section, field)) return;
            var itemList = this[section];
            for (var i = 0; i < itemList.Count; i++)
            {
                if (itemList[i].Field.Equals(field))
                {
                    itemList.RemoveAt(i);
                }
            }
            this[section] = itemList;
        }

        #endregion

        #region Existance Checking

        /// <summary>
        /// Checks to see if a section exists.
        /// </summary>
        /// <param name="section">The name of the section to check.</param>
        /// <returns>Boolean</returns>
        private bool SectionExists(string section)
        {
            return Sections.Where(sectionName => sectionName.Equals(section)).Count().Equals(1);
        }

        /// <summary>
        /// Checks to see if a field inside of a section exists.
        /// </summary>
        /// <param name="section">The name of the section to check under.</param>
        /// <param name="field">The name of the field to look for.</param>
        /// <returns>Boolean</returns>
        private bool FieldExists(string section, string field)
        {
            return SectionExists(section) && this[section].Where(fieldName => fieldName.Field.Equals(field)).Count().Equals(1);
        }

        #endregion

        #region Get

        /// <summary>
        /// Returns the value of a field in the specified section.
        /// </summary>
        /// <param name="section">The specified section to look under.</param>
        /// <param name="field">The field to look for.</param>
        /// <returns>Object</returns>
        private object GetField(string section, string field)
        {

            // LINQ magic.

            if (SectionExists(section) && FieldExists(section, field))
            {
                // Grab the field and return it.
                return this[section].Where(fieldName => fieldName.Field.Equals(field)).ToList()[0].Value;
            }
            // information for file path is added by scott 130806
            MessageBox.Show("PATH : " + strMainPath + "\n\n○ Invalid Requirement ○ :"+ section + " - " + field );
            return null;
        }

        //*****************************************************************************************
        // Field Value acquisition Functions

        public bool GetBooleanField(string section, string field)
        {
            bool bReturn = false;
            try { bReturn = Convert.ToBoolean(GetField(section, field)); } catch { /* no need any fucking exception message */}
            return bReturn;
        }
        
        public int GetIntegerField(string section, string field)
        {
            int nReturn = 0;
            try{ nReturn = Convert.ToInt32(GetField(section, field)); } catch { /* no need any fucking exception message */}
            return nReturn;
        }

        public double GetDoubleField(string section, string field)
        {
            double dbReturn = 0.0;
            try{   dbReturn = Convert.ToDouble(GetField(section, field));} catch { /* no need any fucking exception message */ }
            return dbReturn;
        }

         public string GetStringField(string section, string field)
        {
            string strReturn = "";
            try { strReturn = GetField(section, field).ToString(); } catch { /* no need any fucking exception message */ }
            return strReturn;
        }

        public string GetFieldAsString(string section, string field)
        {
            string strReturn = "";
            try { strReturn = GetField(section, field).ToString(); }  catch{ /* no need any fucking exception message */ }
            return strReturn;
        }

        //*****************************************************************************************
        // Get Additive Annotation according to user's field name input

        public string GetAnotationString(string section, string field)
        {
            string strReturn = "";

            // Check to see that the field and section exists
            if (!SectionExists(section))
                MessageBox.Show("PATH : " + strMainPath + "\n\n○ Invalid Access ○ :" + section);

            if (!FieldExists(section, field))
                MessageBox.Show("PATH : " + strMainPath + "\n\n○ Invalid Access ○ :" + section + " - " + field);

            // Grab the field list
            var fieldList = this[section];

            for (var i = 0; i < fieldList.Count; i++)
            {
                //Is this it? If not, continue.
                if (!fieldList[i].Field.Equals(field)) continue;

                // Grab the field
                var fieldData = fieldList[i];
                var temp =  fieldData.Comment;

                strReturn = temp.ToString();
                break;
            }

            return strReturn;
        }

        public void Replace(string Section, string Field,string Value, string Comment)
        {
            // Check to see that the field and section exists
            if (!SectionExists(Section))
                MessageBox.Show("○ Invalid Access ○ :" + Section);

            if (!FieldExists(Section, Field))
                MessageBox.Show("○ Invalid Access ○ :" + Section + " - " + Field);

            // Grab the field list
            var fieldList = this[Section];

            for (var i = 0; i < fieldList.Count; i++)
            {
                //Is this it? If not, continue.
                if (!fieldList[i].Field.Equals(Field)) continue;

                var TargetField = fieldList[i];

                TargetField.Value   = Value;

                if (Comment != "")  TargetField.Comment = Comment;
                
                fieldList[i] = TargetField;

                this[Section] = fieldList;
                break;
            }
        }
        #endregion


        #region Set

        /// <summary>
        /// Sets the value of the specified field.
        /// </summary>
        /// <param name="section">The section to look under.</param>
        /// <param name="field">The field to look for.</param>
        /// <param name="value">The value to set.</param>
        private void SetField(string section, string field, object value)
        {

            // Check to see that the field and section exists
            if (!SectionExists(section)) 
                MessageBox.Show("○ Invalid Access ○ :" + section  );

            if (!FieldExists(section, field)) 
                MessageBox.Show("○ Invalid Access ○ :" + section + " - " + field);

            
            // Grab the field list
            var fieldList = this[section];


            // Loop
            for (var i = 0; i < fieldList.Count; i++)
            {
                
                //Is this it? If not, continue.
                if (!fieldList[i].Field.Equals(field)) continue;

                // Grab the field
                var fieldData = fieldList[i];
                fieldData.Value = value;
                fieldList[i] = fieldData;

            }

            // Set it back
            this[section] = fieldList;

        }

        /// <summary>
        /// Sets the value of the specified field to a boolean value.
        /// </summary>
        /// <param name="section">The section to look under.</param>
        /// <param name="field">The field to look for.</param>
        /// <param name="value">The value to set.</param>
        public void SetBooleanField(string section, string field, bool value)
        {
            SetField(section, field, value);
        }

        /// <summary>
        /// Sets the value of the specified field to a numerical value.
        /// </summary>
        /// <param name="section">The section to look under.</param>
        /// <param name="field">The field to look for.</param>
        /// <param name="value">The value to set.</param>
        public void SetNumberField(string section, string field, decimal value)
        {
            SetField(section, field, value);
        }

        /// <summary>
        /// Sets the value of the specified field to a string value.
        /// </summary>
        /// <param name="section">The section to look under.</param>
        /// <param name="field">The field to look for.</param>
        /// <param name="value">The value to set.</param>
        public void SetStringField(string section, string field, string value)
        {
            SetField(section, field, value);
        }

        public void SetDoubleField(string section, string field, double value)
        {
            SetField(section, field, value);
        }
        #endregion

        #region Save and Load

        public void Load(string iniFile)
        {
            // to inform file name when item missing or error are occured.
            strMainPath = iniFile;

            // Read all the lines in, remove all the blank space.
            // enables fucking korean processing : System.Text.Encoding.Defalut
            var rawFileData =
                System.IO.File.ReadAllLines(iniFile, System.Text.Encoding.UTF8).Where(line => !line.Equals(string.Empty) && !line.StartsWith(";"));

            // Define a variable for use later.
            var currentSection = string.Empty;

            // Begin looping data!
            foreach (var line in rawFileData)
            {
                
                // Check
                if (line.StartsWith("["))
                {
                    currentSection = line.TrimStart('[').TrimEnd(']');
                    if (!SectionExists(currentSection)) Add(currentSection);
                }
                else
                {

                    //************************************************************************************
                    // empty line exception 

                    if (line.IndexOf('[') == -1 && line.IndexOf(']') == -1 && line.IndexOf('=') == -1){
                        continue;
                    }

                    //************************************************************************************
                    // parsing

                
                    var HeaderParse = line.Split('=');
                    var ValueParse = HeaderParse[1].Split(';');

                    string strKeyName = HeaderParse[0].Trim();  // Key name 
                    string strValue = ValueParse[0].Trim();   // Value 
                    string strComment = "";

                    if (ValueParse.Length > 1)
                    {
                        strComment = ValueParse[1].Trim();
                    }
                    
               
                    // Try some conversions to store the item as their natural format.
                    bool boolValue;
                    decimal intvalue;

                    
                    // Boolean test
                    if(Boolean.TryParse(strKeyName, out boolValue))
                    {
                        this[currentSection].Add(new Item { Field = strKeyName, Value = boolValue, Comment = strComment });

                        // Move along
                        continue;

                    }

                    // Number test
                    if (Decimal.TryParse(strKeyName, out intvalue))
                    {

                        this[currentSection].Add(new Item { Field = strKeyName, Value = intvalue, Comment = strComment });

                        // Move along
                        continue;

                    }

                    // It's a string, add it and keep going.
                    this[currentSection].Add(new Item { Field = strKeyName, Value = strValue, Comment = strComment });

                }

            }
            
        }
        public void Save(string iniFile)
        {

            if (string.IsNullOrEmpty(iniFile)) return;

            // Okay, create the file stream
            var sw = new System.IO.StreamWriter(iniFile);

            // Loop
            foreach (var section in Sections)
            {
                
                // Start off each section with [sectionName]
                sw.WriteLine(string.Format("[{0}]", section));

                // Now get items.
                var items = this[section];

                // Loop and write data out.
                foreach (var item in items)
                    sw.WriteLine("{0,30}\t\t=\t{1,10};\t{2}", item.Field, item.Value, item.Comment);

                // Blank gap
                sw.WriteLine();

            }

            // All done
            sw.Close();

        }

        #endregion

    }

}
