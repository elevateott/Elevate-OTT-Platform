namespace ElevateOTT.Application.Features.Identity.Tenants.Commands.CreateTenantCommand;

public class CreateTenantCommand : IRequest<Envelope<CreateTenantResponse>>
{

    // TODO
    // validate subdomain
    // check if exists


    #region Public Properties
    public string TenantName { get; set; }
    public string? ChannelName { get; set; }
    public string? FullName { get; set; }
    public string? SubDomain { get; set; }
    public string? HeardAboutUsFrom { get; set; }

    #endregion Public Properties

    #region Public Methods

    public Tenant MapToEntity()
    {
        return new()
        {
            Id = Guid.NewGuid(),
            FullName = FullName,
            SubDomain = SubDomain,
            ChannelName = ChannelName,
            HeardAboutUsFrom = HeardAboutUsFrom
        };
    }

    #endregion Public Methods

    #region Public Classes

    public class CreateTenantCommandHandler : IRequestHandler<CreateTenantCommand, Envelope<CreateTenantResponse>>
    {        

        #region Private Fields

        private readonly ITenantUseCase _tenantUseCase;
        private readonly ITenantResolver _tenantResolver;

        #endregion Private Fields

        #region Public Constructors

        public CreateTenantCommandHandler(ITenantUseCase tenantUseCase, ITenantResolver tenantResolver)
        {
            _tenantUseCase = tenantUseCase;
            _tenantResolver = tenantResolver;
        }

        #endregion Public Constructors

        #region Public Methods

        public async Task<Envelope<CreateTenantResponse>> Handle(CreateTenantCommand request, CancellationToken cancellationToken)
        {
            return await _tenantUseCase.AddTenant(request);
        }

        #endregion Public Methods
    }

    #endregion Public Classes
}
