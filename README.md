# SentenceBuilder

Sentence Builder is a fluent API for constructing complex sentences programmatically. This tool allows users to dynamically create sentences by chaining methods together, making it easy to generate varied and interesting text.

## Features

- Fluent API for easy sentence construction
- Randomized elements to ensure varied outputs
- Methods to add articles, pluralize nouns, and handle possessives
- Customizable with different word lists

## Usage

Here is an example of how to use the Sentence Builder to generate book titles, from my [BookTitleGenerator](https://github.com/kedga/BookTitleGenerator) project:

```csharp
public static string BookTitlePattern0() => new SentenceBuilder()
    .PickOne(Names.AllFirstNames.PickOne())
    .PickOne(Names.AllLastNames.PickOne())
    .Add("and")
    .AddArticle(ArticleType.Definite)
    .PickOne(PersonsObject, ObjectOfConcept, AdjectiveObject)
    .Build().CapitalizeFirst();

static SentenceBuilder ObjectOfConcept() => new SentenceBuilder()
    .AddRandom(Words.Object)
    .Add("of")
    .AddRandom(Words.Concept);

static SentenceBuilder PersonsObject() => new SentenceBuilder()
    .AddRandom(Words.TypeOfPerson).IsPossesive()
    .AddRandom(Words.Object).Pluralize(true);

static SentenceBuilder AdjectiveObject() => new SentenceBuilder()
    .AddRandom(Words.Adjective)
    .AddRandom(Words.Object).Pluralize(true);
```

This code will generate book titles like:

- Elrond Garcia and the Hologram of Graciousness
- Pandora Eriksson and the Butcher's Telescope
- Beatrix Fitzwilliam and the Galactic Paintings

## API Methods

- PickOne(): Randomly picks one from the given options.
- Add(): Adds a specific word to the sentence.
- AddArticle(): Adds an article (definite or indefinite) to the sentence.
- AddRandom(): Adds a random word from the provided array.
- IsPossesive(): Makes the preceding word possessive.
- IsOptional(): Randomly omit the preceding word, with control for rarity.
- Pluralize(): Pluralizes the preceding word.
- Build(): Builds the final sentence as a string.
- CapitalizeFirst(): Capitalizes the first letter of the final sentence.
