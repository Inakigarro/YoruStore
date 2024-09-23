import { Component, OnInit } from "@angular/core";
import { AppService } from "../app.service";
import { Observable } from "rxjs";
import { Item } from "@components/models";

@Component({
	selector: "app-main",
	templateUrl: "./main.component.html",
})
export class MainComponent implements OnInit {
	public loading$: Observable<boolean>;
	constructor(private service: AppService) {}
	public ngOnInit(): void {
		this.loading$ = this.service.loading$;
	}
}
