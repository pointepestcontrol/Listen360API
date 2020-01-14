using System;

namespace Listen360API
{
    /// <summary>
    /// Provides the base interface for implementation of the <see cref="Franchise"/> class.
    /// </summary>
    public interface IFranchise : IOrganization
    {
        /// <summary>
        /// Gets or sets the radius within which the franchise is permitted to carry out marketing activities.
        /// </summary>
        int? MarketingRadiusInMiles { get; set; }

        /// <summary>
        /// Gets or sets the radius within which the franchise is permitted to operate.
        /// </summary>
        int? OperatingRadiusInMiles { get; set; }

        /// <summary>
        /// Gets a specific customer.
        /// </summary>
        /// <param name="id">The customer's unique identifier.</param>
        /// <returns>The corresponding customer.</returns>
        ICustomer GetCustomerById(long id);

        /// <summary>
        /// Finds a specific <see cref="Customer"/> using the unique value used by your business to identify the customer.
        /// </summary>
        /// <param name="reference">Your unique customer reference.</param>
        /// <returns>The corresponding customer if found; otherwise <b>null</b>.</returns>
        ICustomer FindCustomerByReference(string reference);

        /// <summary>
        /// Gets the first page of up to 100 customers.
        /// </summary>
        /// <returns>The customers on the page.</returns>
        ICustomer[] GetCustomers();

        /// <summary>
        /// Gets a specific page of up to 100 customers.
        /// </summary>
        /// <param name="page">The page number.</param>
        /// <returns>The customers on the page.</returns>
        ICustomer[] GetCustomers(int page);

        /// <summary>
        /// Gets a specific <see cref="Technician"/>.
        /// </summary>
        /// <param name="id">The technician's unique identifier.</param>
        /// <returns>The corresponding technician.</returns>
        ITechnician GetTechnicianById(int id);

        /// <summary>
        /// Finds a specific <see cref="Technician"/> using the unique value used by your business to identify the technician.
        /// </summary>
        /// <param name="reference">Your unique technician reference.</param>
        /// <returns>The corresponding technician if found; otherwise <b>null</b>.</returns>
        ITechnician FindTechnicianByReference(string reference);

        /// <summary>
        /// Gets the first page of up to 100 technicians.
        /// </summary>
        /// <returns>The technicians on the page.</returns>
        ITechnician[] GetTechnicians();

        /// <summary>
        /// Gets a specific page of up to 100 technicians.
        /// </summary>
        /// <param name="page">The page number.</param>
        /// <returns>The technicians on the page.</returns>
        ITechnician[] GetTechnicians(int page);

        /// <summary>
        /// Gets a specific <see cref="Job"/>.
        /// </summary>
        /// <param name="id">The job's unique identifier.</param>
        /// <returns>The corresponding job.</returns>
        IJob GetJobById(int id);

        /// <summary>
        /// Finds a specific <see cref="Job"/> using the unique value used by your business to identify the job.
        /// </summary>
        /// <param name="reference">Your unique job reference.</param>
        /// <returns>The corresponding job if found; otherwise <b>null</b>.</returns>
        IJob FindJobByReference(string reference);

        /// <summary>
        /// Gets the first page of up to 100 jobs.
        /// </summary>
        /// <returns>The jobs on the page.</returns>
        IJob[] GetJobs();

        /// <summary>
        /// Gets a specific page of up to 100 jobs.
        /// </summary>
        /// <param name="page">The page number.</param>
        /// <returns>The jobs on the page.</returns>
        IJob[] GetJobs(int page);
    }
}
