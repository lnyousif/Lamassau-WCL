using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Lamassau.Web.UI.WebControls.Validators
{
	public class RequiredIfValidator : BaseValidator
	{
		/// <summary>
		/// In the BaseValidator's Render method, this method gets
		/// called to make sure all of the required properties have been set.
		/// The BaseValidator's implementation raises an exception if any
		/// required properties aren't set, so that's why I call the base class
		/// implementation but don't keep the return value.
		/// </summary>
		/// <returns>
		/// True if the required properties are all set. The method will raise
		/// an exception if any properties are missing.
		/// </returns>
		protected override bool ControlPropertiesValid()
		{
			base.ControlPropertiesValid();
			
			if (ControlToCompare == "")
			{
				throw new HttpException(string.Format("The ControlToCompare property of '{0}' cannot be blank.", this.ClientID));
			}

			//It's acceptable for TriggerValue to be blank, so that's why 
			//I compare against null.
			if (TriggerValue == null)
			{
				throw new HttpException(string.Format("The TrigerValue property of {0} cannot be null.", this.ClientID));
			}

			return true;
		}

		/// <summary>
		/// Right before the control is going to render, I add my client-side validation
		/// script if the browser is capable of handling it.
		/// </summary>		
		protected override void OnPreRender(EventArgs e)
		{
			base.OnPreRender (e);

			if (RenderUplevel)
			{
				string script = "\r\n<script language=\"javascript\">\r\n" +
					"	function RequiredIfValidatorEvaluateIsValid(val){\r\n" +
					"		if (val.controltocompare != \"\") {\r\n" +
					"			if (document.getElementById(val.controltocompare).value == val.triggervalue) {\r\n" + 
					"				return RequiredFieldValidatorEvaluateIsValid(val);\r\n" +
					"			} else {\r\n" +
					"				return true;\r\n" +
					"			}\r\n" +
					"		} else {\r\n" + 
					"			return true;\r\n" +
					"		}\r\n" +
					"	}\r\n" +
					"</script>\r\n";

				if (!this.Page.ClientScript.IsClientScriptBlockRegistered("__RequiredIfValidatorMethod"))
				{
                    this.Page.ClientScript.RegisterClientScriptBlock(this.GetType(),"__RequiredIfValidatorMethod", script);
				}
			}
		}

		/// <summary>
		/// Here I add extra attributes to the client-side html of the control if 
		/// client-side validation is going to happen.
		/// </summary>		
		protected override void AddAttributesToRender(HtmlTextWriter writer)
		{	
			base.AddAttributesToRender(writer);
			
			if (this.RenderUplevel) 
			{
				//this attribute is needed by the RequiredFieldValidator's
				//validation method.
				writer.AddAttribute("initialvalue", InitialValue);

				//this is the method that asp.net calls when validating my
				//control on the client-side
				writer.AddAttribute("evaluationfunction", "RequiredIfValidatorEvaluateIsValid");

				//attributes that my client-side method will use to validate itself.
				writer.AddAttribute("controltocompare", this.GetControlRenderID(ControlToCompare));
				writer.AddAttribute("triggervalue", TriggerValue);
			}
		}

		/// <summary>
		/// This method is the server-side validation method.
		/// </summary>
		/// <returns>
		/// True if the validation succeeded, false otherwise.
		/// </returns>
		protected override bool EvaluateIsValid()
		{
			//first see if our trigger condition is true
			if (CheckCompareCondition())
			{
				//if the condition was true, do exactly what the 
				//RequiredFieldValidator normally does.

				string controlValue;

				controlValue = this.GetControlValidationValue(this.ControlToValidate);

				if (controlValue == null)
				{
					return true;
				}
				else
				{
					return controlValue.Trim() != this.InitialValue.Trim();
				}
			}
			else
			{
				//if the trigger condition was false, then the field isn't required,
				//so the validation is successful.
				return true;
			}						
		}

		/// <summary>
		/// This checks to see if the value of the ControlToCompare is equal
		/// to our TriggerValue. If the two are equal, it means that the 
		/// ControlToValidate is now required.
		/// </summary>
		/// <returns>
		/// True if the value of ControlToCompare is equal to TriggerValue, false
		/// otherwise.
		/// </returns>
		private bool CheckCompareCondition()
		{
			bool isRequired = false;

			string compareValue = this.GetControlValidationValue(ControlToCompare);

			if (compareValue == TriggerValue)
			{
				isRequired = true;
			}

			return isRequired;
		}

		/// <summary>
		/// This is the control whose value should be compared against to determine
		/// if the ControlToValidate is required.
		/// </summary>
		public string ControlToCompare
		{
			get
			{
				string temp = (string)ViewState["ControlToCompare"];

				if (temp != null)
				{
					return temp;
				}
				else
				{
					return string.Empty;
				}
			}
			set
			{
				ViewState["ControlToCompare"] = value;
			}
		}

		/// <summary>
		/// This is the equivalent of the RequiredFieldValidator's InitialValue
		/// property.
		/// </summary>
		public string InitialValue
		{
			get
			{
				string temp = (string)ViewState["InitialValue"];

				if (temp != null)
				{
					return temp;
				}
				else
				{
					return string.Empty;
				}
			}
			set
			{
				ViewState["InitialValue"] = value;
			}
		}

		/// <summary>
		/// This is the value that the ControlToCompare's value must be equal to in
		/// order for the ControlToValidate to be required.
		/// </summary>
		public string TriggerValue
		{
			get
			{
				return (string)ViewState["TriggerValue"];
			}
			set
			{
				ViewState["TriggerValue"] = value;
			}
		}
	}
}
