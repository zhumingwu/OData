﻿#region License
// 
// Copyright (c) 2013, Bzway team
// 
// Licensed under the BSD License
// See the file LICENSE.txt for details.
// 
#endregion
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using OpenData.Extensions;
namespace OpenData.Framework
{
    public class BzwayModelMetadata : DataAnnotationsModelMetadata
    {
        private string _description;

        public BzwayModelMetadata(DataAnnotationsModelMetadataProvider provider, Type containerType, Func<object> modelAccessor, Type modelType, string propertyName, DisplayColumnAttribute displayColumnAttribute, IEnumerable<Attribute> attributes)
            : base(provider, containerType, modelAccessor, modelType, propertyName, displayColumnAttribute)
        {
            var descAttr = attributes.OfType<DescriptionAttribute>().SingleOrDefault();
            _description = descAttr != null ? descAttr.Description : "";

            DataSourceAttribute = attributes.OfType<DataSourceAttribute>().SingleOrDefault();

            var enumAttribute = attributes.OfType<EnumDataTypeAttribute>().SingleOrDefault();
            if (enumAttribute != null)
            {
                DataSource = new EnumTypeSelectListDataSource(enumAttribute.EnumType);
            }

            Attributes = attributes;

            var defaultValueAttr = attributes.OfType<DefaultValueAttribute>().SingleOrDefault();
            var _displayAttr = attributes.OfType<DisplayAttribute>().SingleOrDefault();
            this._displayName = _displayAttr != null ? _displayAttr.Name : "";

            DefaultValue = defaultValueAttr != null ? defaultValueAttr.Value : this.ModelType.GetDefaultValue();

            this.AdditionalValues["DefaultValue"] = DefaultValue;
        }
        private string _displayName;
        public override string DisplayName
        {
            get
            {
                return this._displayName;
            }
            set
            {
                this._displayName = value;
            }
        }
        public virtual object DefaultValue { get; set; }


        // here's the really important part
        public override string Description
        {
            get
            {
                return _description;
            }
            set
            {
                _description = value;
            }
        }

        public DataSourceAttribute DataSourceAttribute { get; private set; }

        public ISelectListDataSource DataSource { get; private set; }

        public IEnumerable<Attribute> Attributes { get; private set; }
    }

}
