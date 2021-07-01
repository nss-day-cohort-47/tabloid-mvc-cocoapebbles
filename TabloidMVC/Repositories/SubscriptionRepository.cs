using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TabloidMVC.Models;
using TabloidMVC.Utils;

namespace TabloidMVC.Repositories
{
    public class SubscriptionRepository : BaseRepository, ISubscriptionRepository
    {
        public SubscriptionRepository(IConfiguration config) : base(config) { }
        public void AddSubscription(Subscription subscription)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                    INSERT INTO Subscription (
                        SubscriberUserProfileId, ProviderUserProfileId, BeginDateTime, EndDateTime )
                    OUTPUT INSERTED.ID
                    VALUES (
                        @SubscriberUserProfileId, @ProviderUserProfileId, @BeginDateTime, @EndDateTime)
                    ";

                    cmd.Parameters.AddWithValue("@SubscriberUserProfileId", subscription.SubscriberUserProfileId);
                    cmd.Parameters.AddWithValue("@ProviderUserProfileId", subscription.ProviderUserProfileId);
                    cmd.Parameters.AddWithValue("@BeginDateTime", subscription.BeginDateTime);
                    cmd.Parameters.AddWithValue("@EndDateTime", DbUtils.ValueOrDBNull(subscription.EndDateTime));

                    subscription.Id = (int)cmd.ExecuteScalar();
                }
            }
        }
    }
}
