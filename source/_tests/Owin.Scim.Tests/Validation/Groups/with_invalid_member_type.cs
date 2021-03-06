﻿namespace Owin.Scim.Tests.Validation.Groups
{
    using System.Linq;

    using Machine.Specifications;

    using Model.Groups;

    using v2.Model;

    /// <summary>
    /// valid values for member.type are User/Group
    /// </summary>
    public class with_invalid_member_type : when_creating_a_group
    {
        Establish ctx = () =>
        {
            Group = new ScimGroup2
            {
                DisplayName = "blue man",
                Members = new []{new Member {Value="some value", Type = "NotUser"} }
            };
        };

        It should_be_invalid = () => ((bool)Result).ShouldEqual(false);

        It should_indicate_type_is_wrong =
            () => Result.Errors
                    .First(e => e.Detail.Contains("member.type"))
                    .ScimType
                    .ShouldEqual(Model.ScimErrorType.InvalidSyntax);
    }
}