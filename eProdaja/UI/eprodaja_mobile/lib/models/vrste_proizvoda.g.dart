// GENERATED CODE - DO NOT MODIFY BY HAND

part of 'vrste_proizvoda.dart';

// **************************************************************************
// JsonSerializableGenerator
// **************************************************************************

VrsteProizvoda _$VrsteProizvodaFromJson(Map<String, dynamic> json) =>
    VrsteProizvoda()
      ..vrstaId = (json['vrstaId'] as num?)?.toInt()
      ..naziv = json['naziv'] as String?;

Map<String, dynamic> _$VrsteProizvodaToJson(VrsteProizvoda instance) =>
    <String, dynamic>{
      'vrstaId': instance.vrstaId,
      'naziv': instance.naziv,
    };
