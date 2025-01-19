// GENERATED CODE - DO NOT MODIFY BY HAND

part of 'proizvod.dart';

// **************************************************************************
// JsonSerializableGenerator
// **************************************************************************

Proizvod _$ProizvodFromJson(Map<String, dynamic> json) => Proizvod(
      proizvodId: (json['proizvodId'] as num?)?.toInt(),
      naziv: json['naziv'] as String?,
    )
      ..slika = json['slika'] as String?
      ..sifra = json['sifra'] as String?
      ..cijena = (json['cijena'] as num?)?.toDouble()
      ..vrstaId = (json['vrstaId'] as num?)?.toInt()
      ..jedinicaMjereId = (json['jedinicaMjereId'] as num?)?.toInt();

Map<String, dynamic> _$ProizvodToJson(Proizvod instance) => <String, dynamic>{
      'proizvodId': instance.proizvodId,
      'naziv': instance.naziv,
      'slika': instance.slika,
      'sifra': instance.sifra,
      'cijena': instance.cijena,
      'vrstaId': instance.vrstaId,
      'jedinicaMjereId': instance.jedinicaMjereId,
    };
