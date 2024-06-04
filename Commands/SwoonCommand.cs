using COTL_API.CustomFollowerCommand;
using ForceFriendship.Helpers;
using System.Collections.Generic;
using System.IO;

namespace ForceFriendship.Commands
{
    internal class SwoonCommand : CustomFollowerCommand
    {
        public override string InternalName => "Make_Lovers";

        public override List<FollowerCommandCategory> Categories { get; } = new() { FollowerCommandCategory.MAKE_DEMAND_COMMAND };
        public override string GetTitle(Follower follower) { return "Enthrall"; } // thanks ixJhiro
        public override string GetDescription(Follower follower) { return $"Force the {follower} to become lovers with someone"; }
        public override Sprite CommandIcon => TextureHelper.CreateSpriteFromPath(
            Path.Combine(Plugin.PluginPath, "Assets", "love.png"));

        private readonly int _levelOfFriendship = 10;

        public override void Execute(interaction_FollowerInteraction interaction, FollowerCommands finalCommand)
        {
            ChangeRelationship(IDAndRelationship.RelationshipState.Lovers, _levelOfFriendship, interaction.follower, "Select follower to be charmed");
            base.Execute(interaction, finalCommand);
        }

        public override bool ShouldAppearFor(Follower follower)
        {
            return follower.IsSleeping();
        }
    }
}
 