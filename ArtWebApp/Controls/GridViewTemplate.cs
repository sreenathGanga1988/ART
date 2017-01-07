using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ArtWebApp.Controls
{
    public class GridViewTemplate : ITemplate
    {
        //A variable to hold the type of ListItemType.
        ListItemType _templateType;

        //A variable to hold the column name.
        string _columnName;

        //Constructor where we define the template type and column name.
        public GridViewTemplate(ListItemType type, string colname)
        {
            //Stores the template type.
            _templateType = type;

            //Stores the column name.
            _columnName = colname;
        }

        void ITemplate.InstantiateIn(System.Web.UI.Control container)
        {
            switch (_templateType)
            {
                case ListItemType.Header:
                    //Creates a new label control and add it to the container.
                    Label lbl = new Label();            //Allocates the new label object.
                    lbl.Text = _columnName;
                    lbl.CssClass = "Headerclass";
                        //Assigns the name of the column in the lable.
                    container.Controls.Add(lbl);        //Adds the newly created label control to the container.
                    break;

                case ListItemType.Item:
                    //Creates a new text box control and add it to the container.
                    TextBox tb1 = new TextBox();                            //Allocates the new text box object.
                    tb1.DataBinding += new EventHandler(tb1_DataBinding);   //Attaches the data binding event.
                    tb1.Columns =6;                                        //Creates a column with size 4.
                                                                           // tb1.Width = System.Web.UI.WebControls.Unit.Percentage(100);
                    tb1.Width = 100;
                    tb1.Wrap = true;
                    tb1.ID = "txt_" + _columnName;
                    if(_columnName== "ColorTotal")
                    {
                        tb1.CssClass = "ColorTotal";
                    }
                    else if (_columnName == "Color")
                    {
                        tb1.CssClass = "Color";
                    }
                    else
                    {
                        tb1.CssClass = "txtCalQty";
                        tb1.Attributes.Add("onkeypress", "return isNumberKey(event,this)");
                        tb1.Attributes.Add("onkeyup", "sumofQty(this)");
                    }
                   
                    container.Controls.Add(tb1);                            //Adds the newly created textbox to the container.
                   
                    break;

                case ListItemType.EditItem:
                    //As, I am not using any EditItem, I didnot added any code here.
                    break;

                case ListItemType.Footer:
                    CheckBox chkColumn = new CheckBox();
                    chkColumn.ID = "Chk" + _columnName;
                    container.Controls.Add(chkColumn);
                    break;
            }
        }

        /// <summary>
        /// This is the event, which will be raised when the binding happens.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void tb1_DataBinding(object sender, EventArgs e)
        {
            try
            {
                //TextBox txtdata = (TextBox)sender;
                //GridViewRow container = (GridViewRow)txtdata.NamingContainer;
                //object dataValue = DataBinder.Eval(container.DataItem, _columnName.ToString ());
                //if (dataValue != DBNull.Value)
                //{
                //    txtdata.Text = dataValue.ToString();
                //}
            }
            catch (Exception)
            {
                
                
            }
        }
    }
}
