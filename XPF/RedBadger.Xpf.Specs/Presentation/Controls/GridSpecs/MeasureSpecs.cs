//-------------------------------------------------------------------------------------------------
// <auto-generated> 
// Marked as auto-generated so StyleCop will ignore BDD style tests
// </auto-generated>
//-------------------------------------------------------------------------------------------------

#pragma warning disable 169
// ReSharper disable InconsistentNaming
// ReSharper disable UnusedMember.Global
// ReSharper disable UnusedMember.Local

namespace RedBadger.Xpf.Specs.Presentation.Controls.GridSpecs
{
    using Machine.Specifications;

    using Moq;
    using Moq.Protected;

    using RedBadger.Xpf.Internal;
    using RedBadger.Xpf.Presentation;
    using RedBadger.Xpf.Presentation.Controls;

    using It = Machine.Specifications.It;

    [Subject(typeof(Grid), "Measure")]
    public class when_a_column_index_is_specified_greater_than_the_number_of_columns_available : a_Grid
    {
        private const double Column2Width = 20d;

        private static Mock<UIElement> child;

        private Establish context = () =>
            {
                Subject.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(10d) });
                Subject.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(Column2Width) });

                child = new Mock<UIElement> { CallBase = true };

                Grid.SetColumn(child.Object, 2);

                Subject.Children.Add(child.Object);
            };

        private Because of = () => Subject.Measure(AvailableSize);

        private It should_put_it_in_the_last_column =
            () =>
            child.Protected().Verify(
                MeasureOverride, Times.Once(), ItExpr.Is<Size>(size => size.Width.Equals(Column2Width)));
    }

    [Subject(typeof(Grid), "Measure")]
    public class when_a_row_index_is_specified_greater_than_the_number_of_rows_available : a_Grid
    {
        private const double Row2Height = 20d;

        private static Mock<UIElement> child;

        private Establish context = () =>
            {
                Subject.RowDefinitions.Add(new RowDefinition { Height = new GridLength(10d) });
                Subject.RowDefinitions.Add(new RowDefinition { Height = new GridLength(Row2Height) });

                child = new Mock<UIElement> { CallBase = true };

                Grid.SetRow(child.Object, 2);

                Subject.Children.Add(child.Object);
            };

        private Because of = () => Subject.Measure(AvailableSize);

        private It should_put_it_in_the_last_row =
            () =>
            child.Protected().Verify(
                MeasureOverride, Times.Once(), ItExpr.Is<Size>(size => size.Height.Equals(Row2Height)));
    }

    [Subject(typeof(Grid), "Measure - Pixel/Auto/Star")]
    public class when_measuring_a_grid_that_has_columns_of_mixed_pixel_auto_and_star : a_Grid
    {
        private const int Column1PixelWidth = 30;

        private const int Column2ChildWidth = 20;

        private static readonly Mock<UIElement>[] children = new Mock<UIElement>[4];

        private static readonly double expectedProportionalWidth = (AvailableSize.Width - Column1PixelWidth -
                                                                    Column2ChildWidth) / 2;

        private Establish context = () =>
            {
                Subject.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(Column1PixelWidth) });
                Subject.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Auto) });
                Subject.ColumnDefinitions.Add(new ColumnDefinition());
                Subject.ColumnDefinitions.Add(new ColumnDefinition());

                children[0] = new Mock<UIElement> { CallBase = true };
                children[1] = new Mock<UIElement> { CallBase = true };
                children[2] = new Mock<UIElement> { CallBase = true };
                children[3] = new Mock<UIElement> { CallBase = true };

                Grid.SetColumn(children[0].Object, 0);
                Grid.SetColumn(children[1].Object, 1);
                Grid.SetColumn(children[2].Object, 2);
                Grid.SetColumn(children[3].Object, 3);

                children[1].Object.Width = Column2ChildWidth;

                Subject.Children.Add(children[0].Object);
                Subject.Children.Add(children[1].Object);
                Subject.Children.Add(children[2].Object);
                Subject.Children.Add(children[3].Object);
            };

        private Because of = () => Subject.Measure(AvailableSize);

        private It should_give_column_1_the_correct_amount_of_space =
            () =>
            children[0].Protected().Verify(
                MeasureOverride, Times.Once(), ItExpr.Is<Size>(size => size.Width.Equals(Column1PixelWidth)));

        private It should_give_column_2_the_correct_amount_of_space =
            () =>
            children[1].Protected().Verify(
                MeasureOverride, Times.Once(), ItExpr.Is<Size>(size => size.Width.Equals(Column2ChildWidth)));

        private It should_give_column_3_the_correct_amount_of_space =
            () =>
            children[2].Protected().Verify(
                MeasureOverride, Times.Once(), ItExpr.Is<Size>(size => size.Width.IsCloseTo(expectedProportionalWidth)));

        private It should_give_column_4_the_correct_amount_of_space =
            () =>
            children[3].Protected().Verify(
                MeasureOverride, Times.Once(), ItExpr.Is<Size>(size => size.Width.IsCloseTo(expectedProportionalWidth)));
    }

    [Subject(typeof(Grid), "Measure - Pixel/Auto/Star")]
    public class when_measuring_a_grid_that_has_rows_of_mixed_pixel_auto_and_star : a_Grid
    {
        private const int Row1PixelHeight = 30;

        private const int Row2ChildHeight = 20;

        private static readonly Mock<UIElement>[] children = new Mock<UIElement>[4];

        private static readonly double expectedProportionalHeight = (AvailableSize.Height - Row1PixelHeight -
                                                                     Row2ChildHeight) / 2;

        private Establish context = () =>
            {
                Subject.RowDefinitions.Add(new RowDefinition { Height = new GridLength(Row1PixelHeight) });
                Subject.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Auto) });
                Subject.RowDefinitions.Add(new RowDefinition());
                Subject.RowDefinitions.Add(new RowDefinition());

                children[0] = new Mock<UIElement> { CallBase = true };
                children[1] = new Mock<UIElement> { CallBase = true };
                children[2] = new Mock<UIElement> { CallBase = true };
                children[3] = new Mock<UIElement> { CallBase = true };

                Grid.SetRow(children[0].Object, 0);
                Grid.SetRow(children[1].Object, 1);
                Grid.SetRow(children[2].Object, 2);
                Grid.SetRow(children[3].Object, 3);

                children[1].Object.Height = Row2ChildHeight;

                Subject.Children.Add(children[0].Object);
                Subject.Children.Add(children[1].Object);
                Subject.Children.Add(children[2].Object);
                Subject.Children.Add(children[3].Object);
            };

        private Because of = () => Subject.Measure(AvailableSize);

        private It should_give_row_1_the_correct_amount_of_space =
            () =>
            children[0].Protected().Verify(
                MeasureOverride, Times.Once(), ItExpr.Is<Size>(size => size.Height.Equals(Row1PixelHeight)));

        private It should_give_row_2_the_correct_amount_of_space =
            () =>
            children[1].Protected().Verify(
                MeasureOverride, Times.Once(), ItExpr.Is<Size>(size => size.Height.Equals(Row2ChildHeight)));

        private It should_give_row_3_the_correct_amount_of_space =
            () =>
            children[2].Protected().Verify(
                MeasureOverride, Times.Once(), ItExpr.Is<Size>(size => size.Height.Equals(expectedProportionalHeight)));

        private It should_give_row_4_the_correct_amount_of_space =
            () =>
            children[3].Protected().Verify(
                MeasureOverride, Times.Once(), ItExpr.Is<Size>(size => size.Height.Equals(expectedProportionalHeight)));
    }
}