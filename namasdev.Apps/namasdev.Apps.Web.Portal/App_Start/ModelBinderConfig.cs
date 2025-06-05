using System;
using System.Web.Mvc;

using namasdev.Web.ModelBinders;

namespace namasdev.Apps.Web.Portal
{
    public class ModelBinderConfig
    {
        public static void Config()
        {
            ModelBinders.Binders.Add(typeof(DateTime), new DateTimeModelBinder());
            ModelBinders.Binders.Add(typeof(DateTime?), new DateTimeModelBinder());
            ModelBinders.Binders.Add(typeof(int), new IntegerModelBinder());
            ModelBinders.Binders.Add(typeof(int?), new IntegerModelBinder());
            ModelBinders.Binders.Add(typeof(short), new ShortModelBinder());
            ModelBinders.Binders.Add(typeof(short?), new ShortModelBinder());
            ModelBinders.Binders.Add(typeof(long), new LongModelBinder());
            ModelBinders.Binders.Add(typeof(long?), new LongModelBinder());
            ModelBinders.Binders.Add(typeof(decimal), new DecimalModelBinder());
            ModelBinders.Binders.Add(typeof(decimal?), new DecimalModelBinder());
            ModelBinders.Binders.Add(typeof(double), new DoubleModelBinder());
            ModelBinders.Binders.Add(typeof(double?), new DoubleModelBinder());
        }
    }
}