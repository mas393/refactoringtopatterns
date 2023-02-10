using System;
using System.Collections.Generic;
using EncapsulateClassesWithFactory.MyWork.Descriptors;
using EncapsulateClassesWithFactory.MyWork.Domain;

namespace EncapsulateClassesWithFactory.MyWork.Mappers
{
    public class DescriptorMapper
    {
        protected List<AttributeDescriptor> CreateAttributeDescriptors() {
            var result = new List<AttributeDescriptor>();

            result.Add(AttributeDescriptor.FromInt("remoteId", GetClass()));
            result.Add(AttributeDescriptor.FromDateTime("createdDate", GetClass()));
            result.Add(AttributeDescriptor.FromDateTime("lastChangedDate", GetClass()));
            result.Add(AttributeDescriptor.FromUser("createdBy", GetClass()));
            result.Add(AttributeDescriptor.FromUser("lastChangedBy", GetClass()));
            result.Add(AttributeDescriptor.FromInt("optimisticLockVersion", GetClass()));
            return result;
        }

        private Type GetClass()
        {
            return typeof(DescriptorMapper);
        }
    }
}