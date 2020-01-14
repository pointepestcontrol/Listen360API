using System;
using System.Collections;
using System.Text;

namespace Listen360API
{
    /// <summary>
    /// Provides the base interface for implementation of the <see cref="Job"/> class.
    /// </summary>
    public interface IJob : IModel
    {
        /// <summary>
        /// Gets the unique identifier of the organization responsible for the job.
        /// </summary>
        long OrganizationId { get; }

        /// <summary>
        /// Gets the unique identifier of the customer for whom the job was or will be performed.
        /// </summary>
        long CustomerId  { get; }

        /// <summary>
        /// Gets or sets the unique value used by your business to identify the customer.
        /// </summary>
        string Reference { get; set; }

        /// <summary>
        /// Gets or sets the job's status.
        /// </summary>
        string Status { get; set; }

        /// <summary>
        /// Gets or sets the job's currency code in <a href="http://en.wikipedia.org/wiki/ISO_4217">ISO 4217</a> format.
        /// </summary>
        string Currency { get; set; }

        /// <summary>
        /// Gets or sets the total value of the job.
        /// </summary>
        decimal? Value { get; set; }

        /// <summary>
        /// Gets or sets the date and time at which the job was commenced.
        /// </summary>
        DateTime? CommencedAt { get; set; }

        /// <summary>
        /// Gets or sets the date and time at which the job was completed.
        /// </summary>
        DateTime? CompletedAt { get; set; }

        /// <summary>
        /// Gets a list of technicians responsible for performing the job.
        /// </summary>
        ArrayList Performers { get; }

        /// <summary>
        /// Sets a value to indicate that a survey should be sent no matter what.
        /// </summary>
        bool ForceFeedback { set; }
    }
}
