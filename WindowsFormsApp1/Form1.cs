using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Reflection;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        List<FactCreator> humanFactories = new List<FactCreator>();
        List<Fact> objects = new List<Fact>();
        List<Control> allTextBoxes = new List<Control>();
        Fact newObject;
        List<Type> humanArr = new List<Type>();

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.Height = 590;
            this.Width = 1000;

            humanFactories.Add(new HumanFab());
            humanFactories.Add(new TeacherFab());
            humanFactories.Add(new StudentFab());
            humanFactories.Add(new ProfessorFab());
            humanFactories.Add(new GroupFab());

            foreach (FactCreator item in humanFactories)
            {
                comboBoxClasses.Items.Add(item.GetType().Name);
                humanArr.Add(item.CreateHuman().GetType());           
            }
        }

        private void comboBoxClasses_SelectedIndexChanged(object sender, EventArgs e)
        {
            RemoveComponents(ref allTextBoxes);
            newObject = humanFactories[comboBoxClasses.SelectedIndex].CreateHuman();
            Type newObjectType = newObject.GetType();
            CreateComponents(ref allTextBoxes, newObjectType);
        }
        private void t_click(object sender, EventArgs e)
        {
        }

        public void DrawProp(ref List<Control> item, PropertyInfo itemClass, ref int top, ref int left, int delta)
        {
            item.Add(new Label());
            item[item.Count - 1].Top = top;
            item[item.Count - 1].Left = left;
            item[item.Count - 1].Text = itemClass.Name;
            this.Controls.Add(item[item.Count - 1]);
            top += delta;
            item.Add(new TextBox());
            item[item.Count - 1].Click += new EventHandler(t_click);
            item[item.Count - 1].Top = top;
            item[item.Count - 1].Left = left;
            this.Controls.Add(item[item.Count - 1]);
            top += delta;
        }

        public void DrawField(ref List<Control> item, FieldInfo itemClass, ref int top, ref int left, int delta)
        {
            item.Add(new Label());
            item[item.Count - 1].Top = top;
            item[item.Count - 1].Left = left;
            item[item.Count - 1].Text = itemClass.Name;
            this.Controls.Add(item[item.Count - 1]);
            top += delta;
            item.Add(new TextBox());
            item[item.Count - 1].Click += new EventHandler(t_click);
            item[item.Count - 1].Top = top;
            item[item.Count - 1].Left = left;
            this.Controls.Add(item[item.Count - 1]);
            top += delta;
        }

        public void CreateComponents(ref List<Control> allTextBox, Type newObjectType)
        {
            try
            {
                int left = 200;
                int delta = 25;
                int top = 0;
               
                PropertyInfo[] NewObjectProperties = newObjectType.GetProperties();
                FieldInfo[] NewObjectFields = newObjectType.GetFields();
                foreach (PropertyInfo itemClass in NewObjectProperties)
                {
                    DrawProp(ref allTextBox, itemClass, ref top, ref left, delta);
                }
                    
                foreach (FieldInfo item in NewObjectFields)
                {              
                    int index = humanArr.FindIndex(element => element == item.FieldType);
                    left += 125;
                    if (index != -1)               
                    {
                        int subTop = 35;
                        Fact subObject = humanFactories[index].CreateHuman();
                        Type subObjectType = subObject.GetType();
                        PropertyInfo[] subObjectProperties = subObjectType.GetProperties();

                        allTextBox.Add(new Label());
                        allTextBox[allTextBox.Count - 1].Top = 0;
                        allTextBox[allTextBox.Count - 1].Left = left;
                        allTextBox[allTextBox.Count - 1].Text = item.Name.ToUpper();
                        this.Controls.Add(allTextBox[allTextBox.Count - 1]);

                        foreach (PropertyInfo subItem in subObjectProperties)
                        {
                            DrawProp(ref allTextBox, subItem, ref subTop, ref left, delta);                             
                         }
                    }
                    else
                    {
                        DrawField(ref allTextBox, item, ref top, ref left, delta);                           
                    }
                }
            }
            catch
            {
                MessageBox.Show("Ошибка!");
            }
        }

        public void RemoveComponents(ref List<Control> items)
        {
            foreach (Control item in items)
            {
                item.Dispose();
            }
            items.Clear();
        }
        private void addProp(PropertyInfo subItem, ref Fact subObject, ref int j)
        {
            Type type = subItem.PropertyType;
            j++;
            object o = Convert.ChangeType(allTextBoxes[j].Text, type);
            subItem.SetValue(subObject, o);
            j++;
        }
        private void addField(FieldInfo item, ref Fact subObject, ref int i)
        {
            Type type = item.FieldType;
            i++;
            object o = Convert.ChangeType(allTextBoxes[i].Text, type);
            item.SetValue(newObject, o);
            i++;
        }

        private void btn_add_Click(object sender, EventArgs e)
        {
            Type newObjectType = newObject.GetType();
            FieldInfo[] NewObjectFields = newObjectType.GetFields();
            int i = 0;
            try
            {
                PropertyInfo[] NewObjectProperties = newObjectType.GetProperties();
                foreach (PropertyInfo item in NewObjectProperties)
                {
                    addProp(item, ref newObject, ref i);
                }
                foreach (FieldInfo item in NewObjectFields)
                {
                    int index = humanArr.FindIndex(element => element == item.FieldType);
                    if (index != -1)
                    {
                        i++;
                        Fact subObject = humanFactories[index].CreateHuman();
                        Type subObjectType = subObject.GetType();
                        PropertyInfo[] subObjectProperties = subObjectType.GetProperties();

                        foreach (PropertyInfo subItem in subObjectProperties)
                        {
                            addProp(subItem, ref subObject, ref i);
                        }
                        item.SetValue(newObject, subObject);
                    }
                    else
                    {
                        addField(item, ref newObject, ref i);                       
                    }
                }
                try
                {
                    listBoxObj.Items.Add(newObject.ToString() + ' ' + newObjectType.GetProperty("LastName").GetValue(newObject).ToString());
                    RemoveComponents(ref allTextBoxes);
                    objects.Add(newObject);
                }
                catch
                {
                    try
                    {
                        listBoxObj.Items.Add(newObject.ToString() + ' ' + newObjectType.GetProperty("number").GetValue(newObject).ToString());
                        RemoveComponents(ref allTextBoxes);
                        objects.Add(newObject);
                    }
                    catch
                    {
                        MessageBox.Show("Ошибка!");
                    }
                }
            }
            catch
            {
                MessageBox.Show("Неверно введены данные!");
            }           
        }

        private void btn_edit_Click(object sender, EventArgs e)
        {
            try
            {
                Type newObjectType = objects[listBoxObj.SelectedIndex].GetType();
                FieldInfo[] NewObjectFields1 = newObjectType.GetFields();
                int i = 0;
                PropertyInfo[] NewObjectProperties1 = newObjectType.GetProperties();
                foreach(PropertyInfo item in NewObjectProperties1)
                {
                    addProp(item, ref newObject, ref i);
                }
                foreach (FieldInfo item in NewObjectFields1)
                {
                    int index = humanArr.FindIndex(element => element == item.FieldType);
                    if (index != -1)
                    {
                        i++;
                        Fact subObject = humanFactories[index].CreateHuman();
                        Type subObjectType = subObject.GetType();
                        PropertyInfo[] subObjectProperties = subObjectType.GetProperties();

                        foreach (PropertyInfo subItem in subObjectProperties)
                        {
                            addProp(subItem, ref subObject, ref i);
                        }
                        item.SetValue(newObject, subObject);
                    }
                    else
                    {
                        addField(item, ref newObject, ref i);
                    }
                }                
                try
                {
                    int selectedIndex = listBoxObj.SelectedIndex;
                    string newName = newObjectType.GetProperty("LastName").GetValue(objects[listBoxObj.SelectedIndex]).ToString();
                    string newItem = objects[listBoxObj.SelectedIndex].ToString() + ' ' + newName;
                    listBoxObj.Items.RemoveAt(selectedIndex);
                    listBoxObj.Items.Insert(selectedIndex, newItem);
                    RemoveComponents(ref allTextBoxes);
                }
                catch
                {
                    try
                    {
                        int selectedIndex = listBoxObj.SelectedIndex;
                        string newName = newObjectType.GetProperty("number").GetValue(objects[listBoxObj.SelectedIndex]).ToString();
                        string newItem = objects[listBoxObj.SelectedIndex].ToString() + ' ' + newName;
                        listBoxObj.Items.RemoveAt(selectedIndex);
                        listBoxObj.Items.Insert(selectedIndex, newItem);
                        RemoveComponents(ref allTextBoxes);
                    }
                    catch
                    {
                        MessageBox.Show("Ошибка!");
                    }
                }
            }
            catch
            {
                MessageBox.Show("Неверно введены данные!");
            }        
        }
        
        private void listBoxObj_Click(object sender, EventArgs e)
        {
            try
            {
                RemoveComponents(ref allTextBoxes);
                int left = 200;
                int delta = 25;
                int top = 0;
                MessageBox.Show(System.Convert.ToString(listBoxObj.SelectedIndex));
                Type newObjectType = objects[listBoxObj.SelectedIndex].GetType(); 
                FieldInfo[] NewObjectFields = newObjectType.GetFields();
                PropertyInfo[] NewObjectProperties = newObjectType.GetProperties();

                foreach (PropertyInfo itemClass in NewObjectProperties)
                {
                    DrawProp(ref allTextBoxes, itemClass, ref top, ref left, delta);
                    allTextBoxes[allTextBoxes.Count - 1].Text = itemClass.GetValue(objects[listBoxObj.SelectedIndex]).ToString();
                }

                foreach (FieldInfo item in NewObjectFields)
                {
                    int index = humanArr.FindIndex(element => element == item.FieldType);
                    left += 125;
                    if (index != -1)
                    {
                        int subTop = 35;
                        Fact subObject = (Fact)item.GetValue(objects[listBoxObj.SelectedIndex]);
                        Type subObjectType = subObject.GetType();
                        PropertyInfo[] subObjectProperties = subObjectType.GetProperties();

                        allTextBoxes.Add(new Label());
                        allTextBoxes[allTextBoxes.Count - 1].Top = 0;
                        allTextBoxes[allTextBoxes.Count - 1].Left = left;
                        allTextBoxes[allTextBoxes.Count - 1].Text = item.Name.ToUpper();
                        this.Controls.Add(allTextBoxes[allTextBoxes.Count - 1]);

                        foreach (PropertyInfo subItem in subObjectProperties)
                        {
                            DrawProp(ref allTextBoxes, subItem, ref subTop, ref left, delta);
                            allTextBoxes[allTextBoxes.Count - 1].Text = subItem.GetValue(subObject).ToString();
                        }
                    }
                    else
                    {
                        DrawField(ref allTextBoxes, item, ref top, ref left, delta);
                        allTextBoxes[allTextBoxes.Count - 1].Text = item.GetValue(objects[listBoxObj.SelectedIndex]).ToString();
                    }
                }
            }
            catch
            {
                MessageBox.Show("Ошибка!");
            }
        }

        private void btn_remove_Click(object sender, EventArgs e)
        {
            try
            {
                objects.Remove(objects[listBoxObj.SelectedIndex]);
                listBoxObj.Items.Remove(listBoxObj.SelectedItem);
                RemoveComponents(ref allTextBoxes);
            }
            catch
            {
                MessageBox.Show("Ошибка!");
            }
        }
    }
}
