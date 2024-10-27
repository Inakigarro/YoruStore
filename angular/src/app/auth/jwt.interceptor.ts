import {
	HttpEvent,
	HttpHandler,
	HttpInterceptor,
	HttpRequest,
} from "@angular/common/http";
import { mergeMap, Observable } from "rxjs";
import { AuthService } from "./auth.service";
import { Injectable } from "@angular/core";

@Injectable()
export class JwtInterceptor implements HttpInterceptor {
	public token: Observable<string>;
	constructor(private authService: AuthService) {
		this.token = authService.token$;
	}
	intercept(
		req: HttpRequest<any>,
		next: HttpHandler
	): Observable<HttpEvent<any>> {
		return this.token.pipe(
			mergeMap((token) => {
				if (token) {
					const authReq = req.clone({
						setHeaders: {
							Authorization: `Bearer ${token}`,
						},
					});
					return next.handle(authReq);
				} else {
					return next.handle(req);
				}
			})
		);
	}
}
