namespace PeopleHub.AppHost.Controllers
{
    
    public class UsersController(IMediator mediator) : BaseApiController
    {
        private readonly IMediator _mediator = mediator;
        
    }
}
