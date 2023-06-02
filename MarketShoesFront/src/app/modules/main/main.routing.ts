import {Routes} from "@angular/router";


const routes:Routes =[
  {path:"",redirectTo:"shoes",pathMatch:'full'},
  {path:"shoes", loadChildren:()=>import("./shoes-item/shoes-item.component").then(x=>x.ShoesItemModule)}
]
