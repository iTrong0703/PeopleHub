import { inject } from '@angular/core';
import { HttpErrorResponse, HttpInterceptorFn } from '@angular/common/http';
import { catchError } from 'rxjs';
import { ToastrService } from 'ngx-toastr';
import { NavigationExtras, Router } from '@angular/router';

export const errorInterceptor: HttpInterceptorFn = (req, next) => {
  const toast = inject(ToastrService);
  const router = inject(Router);
  return next(req).pipe(
    catchError((err: HttpErrorResponse) => {
      if(err) {
        switch (err.status) {
          case 400:
            if(err.error.errors) {
              const modelStateErrors = [];
              for (const key in err.error.errors) {
                if (err.error.errors[key]) {
                  modelStateErrors.push(err.error.errors[key]);
                }
              }
              throw modelStateErrors.flat();
            } else {
              toast.error(err.error.detail);
            }
            break;
          case 401:
            toast.error(err.error.detail);
            break;
          case 404:
            router.navigate(['/not-found']);
            break;
          case 500:
            const navigationExtras: NavigationExtras = {state: {error: err.error}};
            router.navigate(['/server-error'], navigationExtras);
            break;
          default:
            toast.error('Something went wrong');
            break;
        }
      }
      throw err;
    })
  );
};
