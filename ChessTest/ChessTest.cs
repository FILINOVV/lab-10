using ChessExample;

namespace ChessTest;

public class CheckerBoardPositionTests
{
    // проверяем null
    [Fact]
    public void ParsingNullPosition()
    {
        var good = CheckerBoardPosition.TryParse(null, null, out var result);

        Assert.False(good);
        Assert.Null(result);
    }

    // простые координаты
    [Fact]
    public void ValidsKoordinats()
    {
        byte x = 1;
        byte y = 3;

        var result = new CheckerBoardPosition(x, y);

        Assert.Equal(x, result.X);
        Assert.Equal(y, result.Y);
    }

    // конвертация в строку
    [Fact]
    public void InvalidToString()
    {
        var pos = new CheckerBoardPosition(3, 6);

        string str = pos.ToString();

        Assert.Equal("C6", str);
    }

    // парсинг строки обратно
    [Fact]
    public void ParsingPosition()
    {
        string input = "B2";

        var result = CheckerBoardPosition.Parse(input, null);

        Assert.Equal(2, result.X);
        Assert.Equal(2, result.Y);
    }

    // невалидные параметры
    [Theory]
    [InlineData(0, 1)]
    [InlineData(10, 1)]
    [InlineData(4, 0)]
    [InlineData(4, 15)]
    [InlineData(10, 15)]
    [InlineData(0, 0)]
    public void ErrorParameter(byte x, byte y)
    {
        Assert.Throws<ArgumentOutOfRangeException>(() => new CheckerBoardPosition(x, y));
    }

    // нормальные параметры
    [Theory]
    [InlineData(5, 1)]
    [InlineData(4, 4)]
    [InlineData(1, 6)]
    [InlineData(8, 8)]
    public void NormalParemeter(byte x, byte y)
    {
        var position = new CheckerBoardPosition(x, y);

        Assert.Equal(x, position.X);
        Assert.Equal(y, position.Y);
    }

    // мусор в строке
    [Theory]
    [InlineData("W1")]
    [InlineData("BB")]
    [InlineData("...")]
    [InlineData(" ")]
    public void ParsingReturnFalse(string input)
    {
        var good = CheckerBoardPosition.TryParse(input, null, out var result);

        Assert.False(good);
        Assert.Null(result);
    }

    // валидный парсинг
    [Theory]
    [InlineData("F5", 6, 5)]
    [InlineData("B4", 2, 4)]
    [InlineData("H6", 8, 6)]
    public void ParsingValidInput(string input, byte x, byte y)
    {
        bool good = CheckerBoardPosition.TryParse(input, null, out var result);

        Assert.True(good);
        Assert.NotNull(result);
        Assert.Equal(x, result.X);
        Assert.Equal(y, result.Y);
    }

    // граничные значения
    [Theory]
    [InlineData(1, 1)]  // минимум
    [InlineData(8, 8)]  // максимум
    public void BorderValues(byte x, byte y)
    {
        var pos = new CheckerBoardPosition(x, y);

        Assert.Equal(x, pos.X);
        Assert.Equal(y, pos.Y);
    }

    // разные буквы
    [Theory]
    [InlineData("A1", 1, 1)]
    [InlineData("H8", 8, 8)]
    [InlineData("D4", 4, 4)]
    public void DifferentLetters(string input, byte expectedX, byte expectedY)
    {
        var good = CheckerBoardPosition.TryParse(input, null, out var result);

        Assert.True(good);
        Assert.Equal(expectedX, result.X);
        Assert.Equal(expectedY, result.Y);
    }
}