using System.Text;

namespace TemplateEngine
{
    public abstract class CustomTemplateBase
    {
        private StringBuilder _buffer;
        public dynamic Model { get; set; }

        protected CustomTemplateBase()
        {
            _buffer = new StringBuilder();
        }

        public string Body
        {
            get 
            {
                return _buffer.ToString();
            }
        }

        public abstract void Execute();

        public virtual void Reset()
        {
            _buffer = new StringBuilder();
            Model = null;
        }

        public virtual void Write(object value)
        {
            WriteLiteral(value);
        }

        public virtual void WriteLiteral(object value)
        {
            _buffer.Append(value);
        }
    }
}