/// <summary>
    /// Required If Attribute used to make a field mandatory based on the other field
    /// </summary>
    /// <seealso cref="System.ComponentModel.DataAnnotations.ValidationAttribute" />
    /// <seealso cref="System.Web.Mvc.IClientValidatable" />
    public class RequiredIfAttribute : ValidationAttribute, IClientValidatable
    {
        /// <summary>
        /// The property name
        /// </summary>
        private readonly string propertyName;
        
        /// <summary>
        /// The desired value
        /// </summary>
        private readonly object desiredValue;

        /// <summary>
        /// The inner attribute
        /// </summary>
        private readonly RequiredAttribute innerAttribute;

        /// <summary>
        /// Initializes a new instance of the <see cref="RequiredIfAttribute"/> class.
        /// </summary>
        /// <param name="propertyName">Name of the property.</param>
        /// <param name="desiredValue">The desired value.</param>
        public RequiredIfAttribute(string propertyName, object desiredValue)
        {
            this.propertyName = propertyName;
            this.desiredValue = desiredValue;
            this.innerAttribute = new RequiredAttribute();
        }

        /// <summary>
        /// Validates by comparing desired value and the dependent property value.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="context">The context.</param>
        /// <returns>Returns true,if is valid else false</returns>
        protected override ValidationResult IsValid(object value, ValidationContext context)
        {
            var dependentValue = context.ObjectInstance.GetType().GetProperty(this.propertyName).GetValue(context.ObjectInstance, null);

            if (dependentValue.ToString() == this.desiredValue.ToString())
            {
                if (!this.innerAttribute.IsValid(value))
                {
                    return new ValidationResult(FormatErrorMessage(context.DisplayName), new[] { context.MemberName });
                }
            }
            return ValidationResult.Success;
        }

        /// <summary>
        /// Configures client validation rule for requiredif custom validator.
        /// </summary>
        /// <param name="metadata">The model metadata.</param>
        /// <param name="context">The controller context.</param>
        /// <returns>
        /// The client validation rules for this validator.
        /// </returns>
        public IEnumerable<ModelClientValidationRule> GetClientValidationRules(ModelMetadata metadata, ControllerContext context)
        {
            var rule = new ModelClientValidationRule
            {
                ErrorMessage = LanguageLabel.GetLabelText("RequiredField"),
                ValidationType = "requiredif",
            };
            rule.ValidationParameters["dependentproperty"] = (context as ViewContext).ViewData.TemplateInfo.GetFullHtmlFieldId(this.propertyName);
            rule.ValidationParameters["desiredvalue"] = this.desiredValue is bool ? this.desiredValue.ToString().ToLower() : this.desiredValue;

            yield return rule;
        }
    }
