using System;
using EncapsulateClassesWithFactory.MyWork.Domain;

namespace EncapsulateClassesWithFactory.MyWork.Descriptors
{
    public class AttributeDescriptor
    {
        readonly string descriptorName;
        readonly Type mapperType;
        readonly Type forType;

        protected AttributeDescriptor(string descriptorName, Type mapperType, Type forType)
        {
            this.descriptorName = descriptorName;
            this.mapperType = mapperType;
            this.forType = forType;
        }

        public static AttributeDescriptor FromInt(string descriptorName, Type mapperType)
        {
            return new DefaultDescriptor(descriptorName, mapperType, typeof(int));
        }

        public static AttributeDescriptor FromDateTime(string descriptorName, Type mapperType)
        {
            return new DefaultDescriptor(descriptorName, mapperType, typeof(DateTime));
        }

        public static AttributeDescriptor FromUser(string descriptorName, Type mapperType)
        {
            return new ReferenceDescriptor(descriptorName, mapperType, typeof(User));
        }

        public string DescriptorName => descriptorName;
    }
}