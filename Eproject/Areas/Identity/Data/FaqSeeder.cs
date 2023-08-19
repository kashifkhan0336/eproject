namespace Eproject.Areas.Identity.Data
{
    using Eproject.Data;
    using Eproject.Models;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.DependencyInjection;
    using System;
    using System.Linq;

    public static class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new EprojectContext(
                serviceProvider.GetRequiredService<DbContextOptions<EprojectContext>>()))
            {
                // Check if data already exists
                if (context.FaqEntries.Any())
                {
                    return;   // Data already seeded
                }

                // Seed initial data
                context.FaqEntries.AddRange(
                    new FaqEntry
                    {
                        Question = "How to Register for the Survey?",
                        Answer = "To register for a survey, visit the survey platform's website or app and sign up with your details."
                    },
    new FaqEntry
    {
        Question = "How to Participate in the Survey?",
        Answer = "Once registered, log in to your account and access available surveys to provide your responses."
    },
    new FaqEntry
    {
        Question = "Notification of New Surveys",
        Answer = "You'll receive email notifications about new surveys if you've provided accurate contact details during registration."
    },
    new FaqEntry
    {
        Question = "Error After Submitting Survey",
        Answer = "If you encounter an error after submitting a survey, contact support for assistance."
    },
    new FaqEntry
    {
        Question = "Unable to Participate in the Survey",
        Answer = "If you can't participate, check if you're a registered user and contact support for technical issues."
    },
    new FaqEntry
    {
        Question = "Registration Request Not Accepted",
        Answer = "If your registration request isn't accepted, contact support for clarification on the reasons."
    },
    new FaqEntry
    {
        Question = "Benefits of Participating",
        Answer = "Participating in surveys may earn you rewards, points, gift cards, or contribute to research."
    },
    new FaqEntry
    {
        Question = "Participating in Competitions",
        Answer = "Follow instructions from the survey platform to participate in competitions related to the survey."
    },
    new FaqEntry
    {
        Question = "Arrears in Participating in Survey",
        Answer = "Complete any outstanding surveys as soon as possible to ensure accurate data collection and eligibility."
    }
                // Add more seed data as needed
                );

                context.SaveChanges();
            }
        }
    }

}
