namespace Logo
{
    /// <remarks/>
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    [System.Xml.Serialization.XmlRootAttribute(Namespace = "", IsNullable = false)]
    public partial class UserList
    {

        private UserListUser[] userField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("User")]
        public UserListUser[] User
        {
            get
            {
                return this.userField;
            }
            set
            {
                this.userField = value;
            }
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class UserListUser
    {

        private string identifierField;

        private string aliasField;

        private string titleField;

        private string typeField;

        private System.DateTime firstCreationTimeField;

        private System.DateTime aliasCreationTimeField;

        private System.DateTime registerTimeField;

        /// <remarks/>
        public string Identifier
        {
            get
            {
                return this.identifierField;
            }
            set
            {
                this.identifierField = value;
            }
        }

        /// <remarks/>
        public string Alias
        {
            get
            {
                return this.aliasField;
            }
            set
            {
                this.aliasField = value;
            }
        }

        /// <remarks/>
        public string Title
        {
            get
            {
                return this.titleField;
            }
            set
            {
                this.titleField = value;
            }
        }

        /// <remarks/>
        public string Type
        {
            get
            {
                return this.typeField;
            }
            set
            {
                this.typeField = value;
            }
        }

        /// <remarks/>
        public System.DateTime FirstCreationTime
        {
            get
            {
                return this.firstCreationTimeField;
            }
            set
            {
                this.firstCreationTimeField = value;
            }
        }

        /// <remarks/>
        public System.DateTime AliasCreationTime
        {
            get
            {
                return this.aliasCreationTimeField;
            }
            set
            {
                this.aliasCreationTimeField = value;
            }
        }

        /// <remarks/>
        public System.DateTime RegisterTime
        {
            get
            {
                return this.registerTimeField;
            }
            set
            {
                this.registerTimeField = value;
            }
        }
    }


}
