namespace OneNote.Sample.Api
{
    public interface IElementBuilderFactory<T>
    {
        /// <summary>
        /// Depending of the document source we can use respective builder
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        IElementBuilder GetBuilder(T source);
    }
}