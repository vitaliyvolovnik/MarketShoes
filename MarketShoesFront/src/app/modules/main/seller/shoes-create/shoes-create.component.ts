import {Component, NgModule} from '@angular/core';
import {CommonModule} from "@angular/common";
import {RouterModule} from "@angular/router";
import {ButtonModule} from "primeng/button";
import {RippleModule} from "primeng/ripple";
import {ChipsModule} from "primeng/chips";
import {InputNumberModule} from "primeng/inputnumber";
import {InputTextareaModule} from "primeng/inputtextarea";
import {CheckboxModule} from "primeng/checkbox";
import {FileUploadModule} from "primeng/fileupload";
import {Characteristic} from "../../../../api/models/Characteristic";
import {FormArray, FormBuilder, FormControl, FormGroup, ReactiveFormsModule, Validators} from "@angular/forms";
import {Product} from "../../../../api/models/Product";
import {ProductHttpService} from "../../../../api/services/product-http.service";
import {SubCharacteristic} from "../../../../api/models/SubCharacteristic";
import {first} from "rxjs";
import {MessageService} from "primeng/api";
import {SubCharacteristicHttpService} from "../../../../api/services/subCharacteristic-http.service";

@Component({
  selector: 'app-shoes-create',
  templateUrl: './shoes-create.component.html',
  styleUrls: ['./shoes-create.component.scss']
})
export class ShoesCreateComponent {
  uploadedFiles: any[] = [];
  characteristics:Characteristic[]= []
  formGroup!: FormGroup;

  constructor(private formBuilder: FormBuilder,
              private productService:ProductHttpService,
              private messageService:MessageService,
               private subCharacteristicService:SubCharacteristicHttpService){
    this.subCharacteristicService.getAll().pipe(first())
      .subscribe({
        next:(characteristics)=>{
          this.characteristics = characteristics as Characteristic[]; console.log(characteristics)
          this.formGroup.controls["Characteristics"] = this.formBuilder.array(Object.keys(this.characteristics.reduce<SubCharacteristic[]>((result, obj) => { return result.concat(obj.subCharacteristics);}, [])).map(key => false))
        },
        error:(err)=>{console.log(err.message())}
      })

  }
  ngOnInit() {
    this.formGroup = this.formBuilder.group({
      Manufacturer: ['', Validators.required],
      Model: ['', Validators.required],
      Quantity: ['', Validators.required],
      Price: ['', Validators.required],
      Description: ['', Validators.required],
      Characteristics: this.formBuilder.array(Object.keys(this.characteristics.reduce<SubCharacteristic[]>((result, obj) => { return result.concat(obj.subCharacteristics);}, [])).map(key => false)),
      Photos: [[]]
    });
  }

  onSubmit() {
    if (this.formGroup.valid) {
      const product: Product = { ...this.formGroup.value };
      let data = new FormData();
      product.Characteristics = this.formGroup.value["Characteristics"].map((x: any[]) => x[0])
      this.productService.create(product, this.formGroup.value.Photos)
        .pipe(first())
        .subscribe({
          next: () => {
            this.messageService.add({severity: 'success', summary: 'Добавлення взуття', detail: 'нове взутя добавлено успішно'});
          },
          error: (err) => {
            this.messageService.add({severity: 'error', summary: 'Добавлення взуття', detail: 'не вдалося добавити взуття'});
          }
        });
    }
  }

  onFileSelect(event:any) {
    const selectedFiles = event.currentFiles;
    console.log(selectedFiles)
    this.formGroup.controls["Photos"].setValue(selectedFiles);


  }

}
@NgModule({
  declarations: [ShoesCreateComponent],
  imports: [
    RouterModule.forChild([{path: "", component: ShoesCreateComponent}]),
    CommonModule,
    ButtonModule,
    RippleModule,
    ChipsModule,
    InputNumberModule,
    InputTextareaModule,
    CheckboxModule,
    FileUploadModule,
    ReactiveFormsModule
  ]
})
export class ShoesCreateModule { }
