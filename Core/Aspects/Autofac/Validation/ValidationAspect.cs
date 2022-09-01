using Castle.DynamicProxy;
using Core.CrossCuttingConcerns.Validation;
using Core.Utilities.Interceptors;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Aspects.Autofac.Validation
{
    public class ValidationAspect : MethodInterception
    {
        private Type _validatorType;
        public ValidationAspect(Type validatorType)
        {
            if (!typeof(IValidator).IsAssignableFrom(validatorType))        // Validator atanabiliyor mu Validator mı ? 
            {
                throw new System.Exception("Bu bir doğrulama sınıfı değil..");
            }

            _validatorType = validatorType;
        }
        protected override void OnBefore(IInvocation invocation)
        {
            var validator = (IValidator)Activator.CreateInstance(_validatorType);       // O validator'ın bir instance'ını oluştur..     ProductValidator 
            var entityType = _validatorType.BaseType.GetGenericArguments()[0];          // O validator'ın çalışma tipini bul ..          Product                 
            var entities = invocation.Arguments.Where(t => t.GetType() == entityType);  // Metodun(Add) argümanları arasından Aranan Validator tipine denk olan tipleri entities içine at
            foreach (var entity in entities)
            {
                ValidationTool.Validate(validator, entity);                       // Herbirini tek tek gez ve validationTool kullanarak validate et.
            }
        }
    }
}
