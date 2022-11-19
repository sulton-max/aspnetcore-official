namespace Domain.Models.Result
{
    public class JsonPatchResult<T>
    {
        public JsonPatchResult(T oldValue, T newValue)
        {
            OldValue = oldValue;
            NewValue = newValue;
        }

        public T OldValue { get; private set; }
        public T NewValue { get; private set; }
    }
}
