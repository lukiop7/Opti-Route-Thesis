import { ErrorHandler, Inject, Injector, Injectable } from "@angular/core";
import { ToastrService } from "ngx-toastr";

@Injectable()
export class AppErrorHandler extends ErrorHandler {

  constructor(@Inject(Injector) private injector: Injector) { 
    super();
  }

  private get toastrService(): ToastrService {
    return this.injector.get(ToastrService);
  }

  public handleError(error: any): void {
    this.toastrService.error(
      "An unexpected error has occurred. The application will be reloaded.",
      "Error",
      {
        progressBar: true,
        timeOut: 1000,
        onActivateTick: true,
      }
    )
    .onHidden
    .subscribe(()=>{
        super.handleError(error);
        window.location.reload();
    });
  }

}