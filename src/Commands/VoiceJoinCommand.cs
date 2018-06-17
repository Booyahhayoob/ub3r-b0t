﻿namespace UB3RB0T.Commands
{
    using System;
    using System.Threading.Tasks;
    using Discord.WebSocket;

    public class VoiceJoinCommand : IDiscordCommand
    {
        public Task<CommandResponse> Process(IDiscordBotContext context)
        {
            var channel = (context.Message.Author as SocketGuildUser)?.VoiceChannel;
            if (channel == null)
            {
                return Task.FromResult(new CommandResponse { Text = "Join a voice channel first" });
            }

            Task.Run(async () =>
            {
                try
                {
                    if (await context.AudioManager.JoinAudioAsync(channel))
                    {
                        await context.Message.Channel.SendMessageAsync($"[Joined {channel.Name}]");
                    }
                }
                catch (Exception ex)
                {
                    // TODO: proper logging
                    Console.WriteLine(ex);
                }
            }).Forget();

            return Task.FromResult((CommandResponse)null);
        }
    }
}
