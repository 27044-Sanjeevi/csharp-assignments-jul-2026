namespace Assignment16AdvancedCSharpConcepts.Task6
{
    /// <summary>
    /// Represents an immutable book record container holding structural catalog data.
    /// </summary>
    /// <param name="title">The primary title designation of the book.</param>
    /// <param name="author">The name string of the person who authored the book.</param>
    /// <param name="isbn">The unique International Standard Book Number string tracking the publication.</param>
    internal record Book(string title, string author, string isbn);
}