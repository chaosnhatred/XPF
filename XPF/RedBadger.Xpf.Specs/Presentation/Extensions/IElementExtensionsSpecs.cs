//-------------------------------------------------------------------------------------------------
// <auto-generated> 
// Marked as auto-generated so StyleCop will ignore BDD style tests
// </auto-generated>
//-------------------------------------------------------------------------------------------------

#pragma warning disable 169
// ReSharper disable InconsistentNaming
// ReSharper disable UnusedMember.Global
// ReSharper disable UnusedMember.Local

namespace RedBadger.Xpf.Specs.Presentation.Extensions
{
    using Machine.Specifications;

    using Moq;

    using RedBadger.Xpf.Presentation;
    using RedBadger.Xpf.Presentation.Controls;
    using RedBadger.Xpf.Presentation.Controls.Primitives;
    using RedBadger.Xpf.Presentation.Extensions;

    using It = Machine.Specifications.It;

    public abstract class a_Hierarchy_of_elements
    {
        protected static Button Button;

        protected static Mock<UIElement> DeepestChild;

        private Establish context = () =>
        {
            DeepestChild = new Mock<UIElement> { CallBase = true };
            Button = new Button { Content = new ContentControl { Content = DeepestChild.Object } };
        };
    }

    [Subject(typeof(IElementExtensions))]
    public class when_searching_for_an_ancestor_of_a_specified_type_that_exists_in_the_tree : a_Hierarchy_of_elements
    {
        private It should_return_the_nearest_ancestor_of_the_requested_type =
            () => DeepestChild.Object.FindNearestAncestorOfType<ButtonBase>().ShouldBeTheSameAs(Button);
    }

    [Subject(typeof(IElementExtensions))]
    public class when_searching_for_an_ancestor_of_a_specified_type_that_does_not_exist_in_the_tree : a_Hierarchy_of_elements
    {
        private It should_return_the_nearest_ancestor_of_the_requested_type =
            () => DeepestChild.Object.FindNearestAncestorOfType<Image>().ShouldBeNull();
    }
}