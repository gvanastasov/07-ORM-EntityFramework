﻿ALTER TABLE [dbo].[Customers] ADD [Age] [int] NOT NULL DEFAULT 20
INSERT [dbo].[__MigrationHistory]([MigrationId], [ContextKey], [Model], [ProductVersion])
VALUES (N'201704171227013_CustomerAddedAge', N'Data.LocalStoreContext',  0x1F8B0800000000000400ED5B4B6FDC3610BE17E87F10746A0B67653B97D4D84DE0AEE36251BFEAB583DE0C5AE2AE8552944A52C61A457F590FFD49FD0B25F5A4484AA2F61537087259F3319CC7C799E168F2EFDFFF8C3FAC22E43C4342C3184FDCA3D1A1EB40ECC74188971337658B37EFDC0FEFBFFD66FC318856CEA772DD5BB18EEFC474E23E31969C781EF59F6004E8280A7D12D378C1467E1C792088BDE3C3C31FBDA3230F72122EA7E538E3DB14B33082D91FFCCF698C7D98B014A0CB38808816E37C669E5175AE400469027C3871CF0003AE738A42C0CF9E43B4701D8071CC00E39C9DDC53386724C6CB79C20700BA7B49205FB70088C282E3937AB92DF387C78279AFDE5892F253CAE26820C1A3B785363C75FB5A3A752B6D717D7DE47A652F42EA4C6713779A1D0189EBA8879D4C11110B73958E72CD8FCAF5078E183DA84CCE9121FE1D38D314B194C009862923001D3837E9230AFD5FE0CB5DFC3BC4139C2224B3C499E2738D013E7443E20412F6720B1705A3B3C075BCE63E4FDD586D93F6E422CC307B7BEC3A57FC70F08860657149DC398B09FC1962480083C10D600C122C68C04C67DAE9CA59E721A14CFC2C8FE438E397C4752EC1EA02E2257B9AB8FCA7EB9C872B189423051BF738E4778A6F6224857D275D803D1D74BA847DFAEB26F0310221DA399B530283904D0109AED2E8514079DB075E81E77099A144397A0E10A4E731A96FD12D44D942FA1426B9FF1989450FF58A731247B7312A7657130F77802C21E3CCC7A6D9799C125FE16CECD5D7B9F3920B52B6175CACFD7AB995B3B83AAAAB207EDF85D100946C031DA5FDCDE828B163CB11FF33487D6664A8987BC8B07DBDA896D69C995768006E59664272E71D13A6BB88FD22029A5428AFC88F9BE16CB0A1CFD655FAD56B5FBAD13DAC746977158BE55F6FA372D65E82DF19A43E09931C743B3EEBD7141482172088B921E1E0707B4342BF7653D00F23C0A3EF0DE1BF8AF4FD9DEBCC7D20085A04F3EEA82779866D3A11D5CFF5F89AF5C261D3A358C64579D3D72BA9E6A48562767335BB9158F9FA5D84062DEEF647111B4C9E521AFB61464412A5CE039AF27FC481D39914E4FA2ED309AE720EC330E1C0E3474FDC1F3485B611AC02614DB04E4D9A440F47A323555649AA6E615BDC431B937DBEA2E6B672347DCCDAD03768C342BD03D4D081CE567B59405562B7E9E90629C5265FDA8E62F22B328D3103217740924B41D9796206AE4C69D33D8585B3A685EB50A51194E790299938759DFA5E6A40D754D22492A9C240205741CFE6025BA6FD1576FBCE970D6364A46975859C6484A64CD22B445A637CA6A801ABCF41552254DAF36C4994589348485654636253380BC1DB12155D03363E6B88D79204AA41D1A1961E27D5A7E23574D3153C0D08B1746643DD992C9882FC2E10F57BAF0D345686F5CA67D515692F2F4997A56BAFA5763DBE0449C2D323A9965D8C38F3BC903D7D331F5EEF8D721A9E4F0D65DF8ADBEA24AE0FB084CA2C3F9A739AD5334596FB084482360D226D99EEA15B1C57799EEA8475F3959EACDC217E572518306A7340B5F6CEB94011CF5933D9A0C169E85BB3CF08000162C89FA7314A23DC968377ED96EAC1321169D89E565DF19549D5A3F694B292AE4C241BB0DF5F5474650AC5903D0DBD5E2B93D36775CA634FB1B60A244F439272B355685A0137F71283419BE505C3016BDEB61BB0E6F54D797F3EF26A545F45C8C1DA6F89D3160668DDB91B1BE8177CE8E56E94AC1AC69427ECE9D5652999583D6A4FA9A84CC9648AA157833025B7187ECB1B49F71AD7BD7BFF6E30D72CDB34824B6366FF566AA65EBA1F961E2B06772BCDDAB85591371A2A4BDA2344D782954D2A3226E3084554870FE5ABC891D7E46B303B3CCDE3B159D42F66541413ABF29DA5B46A463DD8F26DAF35B3CFD796AD8985EE87DC9ACA2FA96C0113DD2FC357058E76A937C646D76BB5CB572B4BD7F517BD0FD935EDD024BC0D0FD2FB327E5588E993BF1F37DA635D5D52C5B2EAD1AE3CCEC7C543B9BFFB4C7B39E74BC457B8F8390CC4AB79FE42198C463910FF405314F2E8592FB804385C40CAF2EF45EEF1E1D1B1D2CEF67A5ACB3C4A0364283498FACBF6FED12BC4A5C7EFFAAC35F013ABD6E5859F01F19F00F92E02ABEF6572EB74726D444CEAD6CA44DFA4576B2346DAFAB106101DD6D9F465404BEE310AF86F96F5180D54BD9C7B49BC6A1F4E663880AB89FB67B6EFC499FDF6206D3D70AE09F74427CEA1F3D7500EE4003F8C817AE726E7EBE1621817EA7E7B5E0677017D19B0DD9AFF3234DC6C444F6DAA59A0180C778B8D9E9A60B39E9A351B54BE0C9C983A44B61011D66DA468960F06F73A681F2CD768C2E0E682446813209E9A52467872AA955138FEB01F2600A97CEB49BA0D0C84222B92EACC194C2016F695E5B239A7A7C051D15510D9A7809D37946CA175649FB66F2F657F3ED3779731F660F94D7B68B6D72FB34724F4159B3F1F1E6CCA155B4785B98148FF8ADDF221406BD3EFE80DCADFF33C15788CB9B1F368D5DE8F626A1D6AED1C3291363726B4341575F5149988B7B59D74771CF5371C1905E96CDDD87153926AD96643405F2F92D629F2BF683B3235187D1651F7DD45D4DE31B453F107B404E9D544EEFBA4FFF1CABD2F0D9735095128C5D06F78BD6ACD0C2FE2D20D2B1C954B944CFC1232107097784A58B8003EE3D33EA434EBD5FE04509A15831E6130C3D7294B52C64586D1236A7CF0154EBCEBFCACEFA9C9F3F83A7BDBD16D88C0D90C458DE41AFF948628A8F83E373CBA5A4888E8503C6F842D9978E62C5F2A4A5731B62454A8AF0A6A77304A102746AFF11C3CC37578BBA7F0022E81FF521685DB89F41BA2A9F6F15908960444B4A051EFE77F720C07D1EAFD7F4B494DABF83D0000 , N'6.1.3-40302')

