﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

using TwitchBot.Models.JSON;

namespace TwitchBot.Libraries
{
    public class TaskJSON
    {
        public static async Task<ChannelJSON> GetChannel(string broadcasterName, string clientID)
        {
            string body = await Program.HttpClient.GetStringAsync("https://api.twitch.tv/kraken/channels/" + broadcasterName + "?client_id=" + clientID);
            ChannelJSON response = JsonConvert.DeserializeObject<ChannelJSON>(body);
            return response;
        }

        public static async Task<RootStreamJSON> GetStream(string broadcasterName, string clientID)
        {
            string body = await Program.HttpClient.GetStringAsync("https://api.twitch.tv/kraken/streams/" + broadcasterName + "?client_id=" + clientID);
            RootStreamJSON response = JsonConvert.DeserializeObject<RootStreamJSON>(body);
            return response;
        }

        public static async Task<HttpResponseMessage> GetFollowerStatus(string broadcasterName, string clientID, string chatterName)
        {
            string apiUriCall = "https://api.twitch.tv/kraken/users/" + chatterName + "/follows/channels/" + broadcasterName + "?client_id=" + clientID;
            return await Program.HttpClient.GetAsync(apiUriCall);
        }

        public static async Task<ChatterInfoJSON> GetChatters(string broadcasterName, string clientID)
        {
            try
            {
                string body = await Program.HttpClient.GetStringAsync("https://tmi.twitch.tv/group/user/" + broadcasterName + "/chatters?client_id=" + clientID);
                ChatterInfoJSON response = JsonConvert.DeserializeObject<ChatterInfoJSON>(body);
                return response;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return new ChatterInfoJSON
                {
                    chatters = new Chatters
                    {
                        viewers = new List<string>(),
                        moderators = new List<string>(),
                        admins = new List<string>(),
                        global_mods = new List<string>(),
                        staff = new List<string>()
                    },
                    chatter_count = 0
                };
            }
        }
    }
}