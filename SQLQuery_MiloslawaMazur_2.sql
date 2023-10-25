--NO FK

--Brand

INSERT INTO DxoMark.Brand ([name])
VALUES 
('Apple'), ('Crosscall'), ('Honor'), ('LG'), ('Microsoft'), ('Oppo'), ('Samsung'), 
('Vivo'), ('Asus'), ('Huawei'), ('Lenovo'), ('Motorola'), ('Sony'), ('Google'), 
('Nokia'), ('Xiaomi'), ('Canon'), ('Nikon'), ('Panasonic'), ('Pixii'), 
('Fujifilm'), ('Olympus'), ('Sigma');


--SpecificationType 

INSERT INTO DxoMark.SpecificationType ([name])
VALUES ('Specification'), ('Segment'), ('Device Type'), ('Format'), 
('Lens Size'), ('Lens Type');

--Functionality

INSERT INTO DxoMark.Functionality ([name])
VALUES 
('Camera'), ('Selfie'), ('Audio'), ('Display'), ('Battery'), 
('Photo'), ('Bokeh'), ('Preview'), ('Zoom'), ('Video'), 
('Friends & Family'), ('Lowlight'), ('Readability'), ('Color'), 
('Motion'), ('Touch'), ('Artifacts'), ('Playback'), ('Recording'), 
('Listening to music'), ('Watching videos'), ('Gaming'), ('Autonomy'), 
('Efficiency'), ('Charging'), ('Overall Sensor Score'), 
('Portrait (Color Depth)'), ('Landscape (Dynamic Range)'), 
('Sports (Low-Light ISO)'),('Overall Camera Score'), ('Overall Speaker Score');

--DeviceFunctionality

INSERT INTO DxoMark.DeviceFunctionality (score, deviceID, functionalityID)
VALUES 
(149, 2, 1), (145, 2, 2), (144, 2, 3), (141, 2, 4), (103, 2, 5), (152, 2, 6), 
(80, 2, 7), (63, 2, 8), (148, 2, 9), (141, 2, 10), (142, 2, 11), (122, 2, 12), 
(146, 3, 1), (145, 3, 2), (142, 3, 3), (149, 3, 4), (119, 3, 5), (143, 3, 6), 
(80, 3, 7), (91, 3, 8), (139, 3, 9), (149, 3, 10), (140, 3, 11), (113, 3, 12), 
(159, 3, 13), (140, 3, 14),
(99, 12, 26), (26.3, 12, 27), (14.6, 12, 28), (2668, 12, 29), 
(152, 4, 30), (143, 5, 30), (126, 6, 30), (129, 7, 30),
(149, 2, 30), (146, 3, 30), (88, 8, 26), (98, 9, 26),
(100, 10, 26), (96, 11, 26), (152, 3, 15), (145, 17, 31),
(135, 18, 31), (126, 19, 31), (122, 20, 31), (136, 6, 6),
(65, 6, 7), (100, 1, 8), (133, 6, 10), (79, 6, 9); 


--Category

INSERT INTO DxoMark.Category ([name])
VALUES ('Test results'), ('Best of'), ('Tech articles');


--MeasurementType

INSERT INTO DxoMark.MeasurementType ([name])
VALUES 
('ISO Sensitivity'), ('SNR 18%'), ('Dynamic Range'), ('Tonal Range'), 
('Color Sensitivity'), ('Full SNR'), ('Color Response'), ('Full CS'), 
('DxOMark Score Map'), ('Sharpness'), 
('Transmission'), ('Distortion'), ('Vignetting'), ('Chromatic aberration');


--Article

INSERT INTO DxoMark.Article (title, [text], [image], video, uploadDate)
VALUES 
--Tech Articles
('DXOMARK Decodes: Software tuning''s pivotal role in display performance', 'Smartphone displays showed a steady stream of enhanced software features and optimizations last year that provided continuous improvements to the user experience. This underscores the fact that a device''s performance doesn''t solely depend on its hardware specifications. Software is a critical “make or break” factor in how well a smartphone works. Manufacturers often issue updates to firmware to correct problems or to optimize one or more aspects of a device''s performance.',
 'https://cdn.dxomark.com/wp-content/uploads/medias/post-139300/MicrosoftTeams-image-27-1024x542.jpg', 
 'https://www.youtube.com/watch?v=BJ3-vyJTInU&t=2s',
 '2023-02-13'),
('Video doorbells: 2022 ranking and comaprisons',
'In 2021 DXOMARK undertook its first benchmark test of home surveillance cameras. Today we are back with an update that includes new camera models to study the evolution of image quality in this constantly growing market.',
'https://cdn.dxomark.com/wp-content/uploads/medias/post-130373/MicrosoftTeams-image-79-1024x683.jpg',
'https://www.youtube.com/watch?v=I8-txL_T2T0',
'2022-11-08'),
('A closer look at the DXOMARK Display protocol',
'Earlier we presented you with the key points of what we test and score in the DXOMARK Display protocol. In this article, we''ll provide a closer look at our process of testing smartphone displays.',
'https://cdn.dxomark.com/wp-content/uploads/medias/post-66011/DXOMARK-Display-Lab-1536x1024-1-1024x768.jpg',
'https://www.youtube.com/watch?v=6oGRmyrLCZU',
'2022-09-20'),
--test results
--no FK
('Google Pixel 6 Pro Camera test',
'The Pixel 6 Pro is the 2021 flagship in Google''s Pixel line of smartphone, featuring a 6.7-inch OLED LTPO display with 120Hz refresh rate and QHD+ resolution, Google''s brand new in-house-developed Tensor chipset and up to 512GB of ROM.',
'https://cdn.dxomark.com/wp-content/uploads/medias/post-96650/google_pixel_6_pro_review_dxomark_image-550x367.jpg',
'https://www.youtube.com/watch?v=xgvWuJeOyjc',
'2022-09-20'),
--FK to Huawei Mate 50 Pro
('Huawei Mate 50 Pro Selfie test',
'We put the Huawei Mate 50 Pro through our rigorous DXOMARK Selfie test suite to measure its performance in photo and video from an end-user perspective',
'https://cdn.dxomark.com/wp-content/uploads/medias/post-134205/Huawei_Mate50Pro-2-550x367.jpg',
'https://www.youtube.com/watch?v=g3LMEDcPyBU',
'2022-12-14');

INSERT INTO DxoMark.Article (title, [text], [image], uploadDate)
VALUES 
--tech articles
('GPS on smartphones: Testing the accuracy of location positioning', 
'Have you ever experienced using your phone''s navigation system to look for an address or landmark, only to discover that what you''re looking for is on the opposite side of the street from where your phone says it is? If so, then it will not surprise you to learn that location positioning on a smartphone is not always accurate.', 
'https://cdn.dxomark.com/wp-content/uploads/medias/post-121012/shutterstock_1007861857-1024x683.jpg',
'2023-02-10'),
('How social media apps change the sound of concert recordings',
'The atmosphere in the darkened stadium starts to thicken with excitement as the crowd awaits the band to take the stage. Amid the chatter, the sounds of a guitar begin to fill the arena and suddenly hundreds of little lights pop up from the audience. These are not the flames of cigarette lighters from yesteryear, but rather the luminance from smartphone screens that are capturing the images and music from the concert.',
'https://cdn.dxomark.com/wp-content/uploads/medias/post-136170/SocMed-app-recording-shutterstock_391367674-1024x683.jpg',
'2023-01-26'),
('Always-on Display: How does it affect battery life?',
'The display is a high power-consuming part of a smartphone. Because of this, most smartphone displays turn off after a relatively brief period of inactivity. The “always-on” feature lets users see certain kinds of information, such as the time, without having to fully wake up the phone.',
'https://cdn.dxomark.com/wp-content/uploads/medias/post-135158/Always-On-featured-image-packshot-review-Recovered-1024x691.jpg',
'2022-12-16'),
('A closer look at how DXOMARK tests the smartphone battery experience',
'This article will take a deeper dive into some of the specifics of the equipment our engineers use and the procedures they follow for testing. We''ll be taking a look at how we test smartphone battery performance.',
'https://cdn.dxomark.com/wp-content/uploads/medias/post-77919/Battery-Photos-press-and-social-29-1024x768.jpg',
'2022-09-20'),
--no FK
('A brief introduction to how we test doorbell cameras',
'Doorbell cameras are becoming the staple of every connected home because they have become the first line of defense in a home security system.',
'https://cdn.dxomark.com/wp-content/uploads/medias/post-129068/shutterstock_1812211684-300x200.jpg',
'2022-10-04'),
--no FK
('Smartphone night photography: A flagship comparison',
'The DXOMARK Camera test protocol covers the most common smartphone camera use cases but usually maintains a fixed scope during several months until it is updated to include newly emerged user scenarios.',
'https://cdn.dxomark.com/wp-content/uploads/medias/post-101224/NIGHT_illustration-1024x768.jpg',
'2021-12-16'),
--test results
--FK to Apple iPhone 14 Pro
('Apple iPhone 14 Pro Max Camera test results',
 'The Apple iPhone 14 Pro Max and iPhone 14 Pro share the same rear camera specs, as well the same A16 Bionic processor, so as expected, the results of the Apple iPhone 14 Pro Max camera are exactly the same as those of the iPhone14 Pro.',
 'https://cdn.dxomark.com/wp-content/uploads/medias/post-126384/MicrosoftTeams-image-13-550x367.jpg', '2022-10-17'),
 --FK to Huawei Mate Pro
 ('Huawei Mate 50 Pro Battery test',
 'We put the Huawei Mate 50 Pro through our rigorous DXOMARK Battery test suite to measure its performance in autonomy, charging and efficiency. In these test results, we will break down how it fared in a variety of tests and several common use cases.',
 'https://cdn.dxomark.com/wp-content/uploads/drafts/post-139691/Huawei_Mate50Pro-5-550x367.jpg',
 '2023-02-20'),
 --FK Sony SRS-XB43
('Sony SRS-XB43 Speaker test: A good performer at soft volumes',
'Last August, Sony renewed its popular EXTRA-BASS portable speaker lineup with three brand new IP67-rated (waterproof, dust-proof, and washable) devices.',
'https://cdn.dxomark.com/wp-content/uploads/medias/post-64414/4096-3072-max-9-550x413.jpg',
'2021-02-02'),
 -- FK Panasonic Lumix DC-S1R
('Panasonic Lumix DC-S1R sensor review',
'The Panasonic Lumix DC-S1R is the company''s high-resolution version of the Lumix DC-S1 full-frame mirrorless camera. It uses the L-mount originally developed by Leica and supports Leica lenses as well as lenses from Sigma and Panasonic.',
'https://cdn.dxomark.com/wp-content/uploads/medias/post-29080/panasonic-s1r.jpg',
'2019-05-07'),
--best of
--FK to Pixel 6
('Best of smartphones [Summer 2022]',
'After a slight pandemic-related slow-down in 2021, 2022 has been a very busy year so far in terms of smartphone launches, leaving consumers who are ready to upgrade their current device to the latest generation with almost too many options to choose from.',
'https://cdn.dxomark.com/wp-content/uploads/medias/post-116107/Featured-image_New-1024x529.png',
'2022-06-17'),
--no FK
('The best ultra-premium smartphones for photography [Summer 2021]',
'Most phones in the ultra-premium category are flagship devices of their respective brands, packed with high-end camera components and designed to showcase the technological prowess of the manufacturers.',
'https://cdn.dxomark.com/wp-content/uploads/medias/post-90166/MicrosoftTeams-image-7-1024x691.jpg',
'2021-08-31'),
--no FK
('Black Friday buyers'' guide: Best wireless speakers',
'Since DXOMARK''s sound-quality testing has expanded to wireless speakers a year ago, our engineers have evaluated the playback abilities of a wide range of compact speakers, from entry-level products to high-end models. ',
'https://cdn.dxomark.com/wp-content/uploads/medias/post-98625/BF-WS-featured-image-packshot-review-1024x691.jpg',
'2021-11-25'),
--no FK
('The best of soundbars 2022',
'With the holidays approaching and Black Friday deals already in full swing, our team of audio experts at DXOMARK has put together something special for our readers.',
'https://cdn.dxomark.com/wp-content/uploads/medias/post-133908/Soundbars-2022-featured-image-1-1024x691.jpg',
'2022-11-23'),
--no FK
('The best lenses for the Nikon D850',
'In October 2017—Nikon''s 100th anniversary year—we published our Nikon D850 sensor review. The D850 scored an excellent 100 points, the first full-frame 35mm sensor to reach 100, which secured its number one position in our Sensor performance ranking at the time for that size sensor.',
'https://cdn.dxomark.com/wp-content/uploads/medias/post-46971/D850_front-1024x841.png',
'2020-05-22');



--ONE FK

--Device:

INSERT INTO DxoMark.Device ([name], launchPrice, launchDate, review, [image], brandID)
VALUES 
--with review
--Sigma 90mm F2.8 DG DN Sony
('90mm F2.8 DG DN Sony', 2200, '2021-09-01', 'Featuring an all-new optical design for Sony and L-mount full-frame mirrorless cameras, the Sigma 90 mm F2.8 DG DN | Contemporary lens is the latest in the range of I sub-series models. As such, it features a compact body and high-quality metal build with knurled control rings and a similarly knurled metal hood, which is included in the price.', 'https://cdn.dxomark.com/dakdata/measures/Optics/Sigma_90mm_F28_DG_DN_Sony/Marketing_PV/Sigma_90mm_F28_DG_DN_Sony.png', 23);
--with video
INSERT INTO DxoMark.Device ([name], launchPrice, launchDate, video, [image], brandID)
VALUES 
--Huawei Mate 50 Pro
('Mate 50 Pro', 1299, '2023-09-01', 'https://youtu.be/DIqpkjy3Olk', 'https://cdn.dxomark.com/wp-content/uploads/medias/post-129588/mate-50-pro.png', 10),
--Apple iPhone 14 Pro
('iPhone 14 Pro', 999, '2022-09-01', 'https://youtu.be/4TFfLR2pnTE', 'https://cdn.dxomark.com/wp-content/uploads/medias/post-126771/Apple-iPhone-14-Pro_FINAL_featured-image-packshot-review-1.jpg', 1);
--without review and image
INSERT INTO DxoMark.Device ([name], launchPrice, launchDate,  [image], brandID)
VALUES 
--Honor Magic5 Pro
('Magic5 Pro', 1199, '2023-02-01', 'https://cdn.dxomark.com/wp-content/uploads/medias/post-140751/Honor-Magic5-Pro_featured-image-packshot-review.jpg', 3),
--Huawei P50 Pro
('P50 Pro', 907, '2021-07-01', 'https://cdn.dxomark.com/wp-content/uploads/medias/post-88973/featured_huaweip50pro.jpg', 10),
--Google Pixel 6
('Pixel 6', 599, '2021-10-01', 'https://cdn.dxomark.com/wp-content/uploads/drafts/post-103489/Google-Pixel-6-featured-image-packshot-review-Recovered.jpg', 14),
--Xiaomi 12 Pro
('12 Pro', 999, '2022-03-01', 'https://cdn.dxomark.com/wp-content/uploads/medias/post-109360/Xiaomi-12-Pro-featured-image-packshot-review-1.jpg', 16),
--camera sensors
--Canon EOS-1D X Mark II
('EOS-1D X Mark II', 6000, '2016-02-01', 'https://cdn.dxomark.com/dakdata/xml/_EOS-1D_X_Mark_II/vignette1.jpg', 17),
--Sony A1
('A1', 6499, '2021-01-01', 'https://cdn.dxomark.com/dakdata/xml/A1/vignette1.jpg', 13),
--Panasonic Lumix DC-S1R
('Lumix DC-S1R', 3700, '2019-02-01', 'https://cdn.dxomark.com/dakdata/xml/Lumix_DC-S1R/vignette1.jpg', 19),
--Sony A7 III
('A7 III', 2000, '2018-02-01', 'https://cdn.dxomark.com/dakdata/xml/A7_III/vignette1.jpg', 13),
--Nikon Z7
('Z7', 3400, '2018-08-01', 'https://cdn.dxomark.com/dakdata/xml/Z7/vignette1.jpg', 18),
--camera lenses
--Nikon NIKKOR Z 58mm f/0.95 S Noct
('NIKKOR Z 58mm f/0.95 S Noct', 8000, '2019-10-01', 'https://cdn.dxomark.com/dakdata/measures/Optics/Nikon_NIKKOR_Z_58mm_F095_S_NOCT/Marketing_PV/Nikon_NIKKOR_Z_58mm_F095_S_NOCT.png', 18),
--Canon EF 85mm f/1.4L IS USM
('EF 85mm f/1.4L IS USM', 1600, '2017-08-01', 'https://cdn.dxomark.com/dakdata/measures/Optics/Canon_EF_85mm_F14L_IS_USM/Marketing_PV/Canon_EF_85mm_F14L_IS_USM.png', 17),
--Sony FE Carl Zeiss Sonnar T* 55mm F1.8 ZA
('FE Carl Zeiss Sonnar T* 55mm F1.8 ZA', 1000, '2013-10-01', 'https://cdn.dxomark.com/dakdata/measures/Optics/Sony_FE_Carl_Zeiss_Sonnar_T_STAR_55mm_F18/Marketing_PV/Sony_FE_Carl_Zeiss_Sonnar_T_STAR_55mm_F18.png', 13),
--Nikon AF-S NIKKOR 105mm f/1.4E ED
('AF-S NIKKOR 105mm f/1.4E ED', 2200, '2016-07-01', 'https://cdn.dxomark.com/dakdata/measures/Optics/Nikon_AF_S_NIKKOR_105mm_F14E_ED/Marketing_PV/Nikon_AF_S_NIKKOR_105mm_F14E_ED.png', 18),
--speakers
--Google Home Max
('Home Max', 399, '2017-10-01', 'https://www.dxomark.com/wp-content/uploads/medias/post-54964/Google-Home-Max-Speaker-dxomark-1.jpg', 14),
--LG XBoomRP4
('XBoomRP4', 399, '2021-09-01', 'https://www.dxomark.com/wp-content/uploads/medias/post-105169/LG-XBoom-360-NEW-featured-image-packshot-review-Recovered.jpg', 4),
--Huawei Sound X
('Sound X', 349, '2020-07-01', 'https://www.dxomark.com/wp-content/uploads/medias/post-56664/huawei-sound-x-speaker-dxomark-1.jpg', 10),
--Sony SRS-XB43
('SRS-XB43', 229, '2020-07-01', 'https://www.dxomark.com/wp-content/uploads/medias/post-64414/srs-xb43.jpg', 13);


--Summary

--pros Honor Magic5 Pro 
INSERT INTO DxoMark.Summary ([text], isPro, deviceID)
VALUES 
('Excellent for photographing family and friends, thanks to accurate skin tones and high details', 1, 4),
('Fast and accurate autofocus, even in challenging light conditions', 1, 4),
('Excellent for low-light imaging', 1, 4),
('Great zoom performances at all ranges', 1, 4),
('Well-rounded display performance, with well-tuned brightness and good picture rendering', 1, 4),
('Excellent management of color and contrast on the screen', 1, 4),
('No display flicker', 1, 4),
--cons Honor Magic5 Pro
('Occasional unnatural rendering of details in images', 0, 4),
('A little shutter lag in high-contrast lighting situations', 0, 4),
('Display tends to lose some contrast when high brightness mode is activated', 0, 4),
--pros google home max
('Excellent maximum volume', 1, 17),
('Good overall tonal balance at all volumes', 1, 17),
('Embedded Smart Sound technology allowing the speaker to adapt to complex acoustics', 1, 17),
('Dynamics are impressively well preserved', 1, 17),
--cons google home max
('Limited wideness', 0, 17),
('No 360° sound', 0, 17),
--pros huawei P50 Pro
('High quality photo results in all test categories', 1, 5),
('Image quality and wide field of view on ultra-wide camera', 1, 5),
('High quality tele zoom', 1, 5),
('Great videos with good stabilization and dynamic range', 1, 5),
('Excellent selfie photos & videos', 1, 5),
('Excellent display experience, particularly in low light and in indoor conditions', 1, 5),
('More than 2 days of autonomy in a moderate use', 1, 5),
('Excellent wired and wireless charging experience', 1, 5),
--cons huawei P50 Pro
('Inaccurate preview : what you see is not as good as what you get', 0, 5),
('No Google Services installed', 0, 5),
('Default brightness is low in outdoor, preventing a pleasant readability of the screen', 0, 5);


--Specification

--Specification
--smartphones
INSERT INTO DxoMark.Specification ([name], displayTop, specificationTypeID)
VALUES ('Dimensions', 0, 1), ('Network', 0, 1), ('Protection', 0, 1), ('Charging', 0, 1), ('Storage', 0, 1), ('Main camera', 0, 1), ('Screen size & resolution', 0, 1), ('Screen type', 0, 1), ('Sound', 0, 1), ('SD card', 0, 1), 
--camera sensors
('Type', 1, 1), ('Announced', 0, 1), ('Sensor type', 0, 1), ('Resolution', 0, 1), ('Color filter array', 0, 1), ('Focal length multiplier', 0, 1), ('Aspect Ratio', 0, 1), ('ISO latitude', 0, 1), ('Shutter type', 0, 1), ('Mount type', 1, 1),
--speakers
('Connectivity', 0, 1), ('Voice assistant', 0, 1),
--Segment
('Essential', 0, 2), ('Advanced', 0, 2), ('High-End', 0, 2), ('Premium', 0, 2), ('Ultra-Premium', 0, 2), 
--DeviceType
('Smartphones', 1, 3), ('Cameras', 1, 3), ('Lenses', 1, 3), ('Speakers', 1, 3),
--Format
('FF', 0, 4), ('APS-C', 0, 4), ('MF', 0, 4),
--LensSize
('Standard', 0, 5), ('Super wide-angle', 0, 5), ('Super telephoto', 0, 5), ('Wide-angle', 0, 5), ('Telephoto', 0, 5),
--LensType
('Prime', 0, 6), ('Zoom', 0, 6);


--TWO FK




--TestedDevice

INSERT INTO DxoMark.TestedDevice (deviceTestedID, deviceTestingID)
VALUES 
-- device tested: Nikon NIKKOR Z 58mm f/0.95 S Noct tested with Nikon Z7
(13, 12), 
-- device testing: Canon EF 85mm f/1.4L IS USM tested with Canon EOS-1D X Mark II
(14, 8);



INSERT INTO DxoMark.DeviceSpecification ([value], deviceID, specificationID)
VALUES 
--Huawei Mate 50 Pro
('162.1 x 75.5 x 8.5mm', 2, 1), ('4G
Dual SIM', 2, 2), ('Water resistant
IP68', 2, 3), ('Wireless', 2, 4), ('256/512 GB', 2, 5), 
('Wide, Ultra-Wide, Tele', 2, 6), ('6.74", 1212*2616 pixels', 2, 7), 
('OLED, 120Hz', 2, 8), ('Stereo Speakers, No 3.5mm jack', 2, 9), ('Yes', 2, 10),
--Nikon Z7
('Hybrid', 12, 11), ('23/08/2018', 12, 12), ('CMOS', 12, 13), ('8288 x 5520', 12, 14), ('RGB', 12, 15), ('1', 12, 16), ('3:2', 12, 17), ('32 - 102400', 12, 18), ('Electronic / Mechanical', 12, 19), ('Nikon Z', 12, 19)
INSERT INTO DxoMark.DeviceSpecification (deviceID, specificationID)
VALUES 
--Huawei Mate 50 Pro
(2, 27), (2, 28),
--Nikon Z7
(12, 29), (12, 32);


--ArticleCategory

INSERT INTO DxoMark.ArticleCategory (categoryID, articleID)
VALUES 
(3, 1), (3, 2), (3, 3), (1, 4), (1, 5), 
(3, 6), (3, 7), (3, 8), (3, 9), (3, 10),
(3, 11), (1, 12), (1, 13), (1, 14), (1, 15),
(2, 16), (2, 17), (2, 18), (2, 19), (2, 20);


--ArticleDevice

INSERT INTO DxoMark.ArticleDevice (deviceID, articleID)
VALUES 
(6, 4), (3, 12), (2, 13), (20, 14), (10, 15), (6, 16), (2, 5);



--DeviceMeasurement

INSERT INTO DxoMark.DeviceMeasurement (infographic, deviceID, measurementTypeID)
VALUES 
--Nikon Z7
('img/device12measurement1', 12, 1),
('img/device12measurement2', 12, 2),
('img/device12measurement3', 12, 3),
('img/device12measurement4', 12, 4),
('img/device12measurement5', 12, 5),
('img/device12measurement6', 12, 6),
('img/device12measurement7', 12, 7),
('img/device12measurement8', 12, 8),
--Sony A1
('img/device9measurement1', 9, 1),
('img/device9measurement2', 9, 2),
('img/device9measurement3', 9, 3),
('img/device9measurement4', 9, 4),
('img/device9measurement5', 9, 5),
('img/device9measurement6', 9, 6),
('img/device9measurement7', 9, 7),
('img/device9measurement8', 9, 8),
--Canon EOS-1D X Mark II
('img/device8measurement1', 8, 1),
('img/device8measurement2', 8, 2),
('img/device8measurement3', 8, 3),
('img/device8measurement4', 8, 4),
('img/device8measurement5', 8, 5),
('img/device8measurement6', 8, 6),
('img/device8measurement7', 8, 7),
('img/device8measurement8', 8, 8),
--Nikon NIKKOR Z 58mm f/0.95 S Noct
('img/device13measurement9', 13, 9),
('img/device13measurement10', 13, 10),
('img/device13measurement11', 13, 11),
('img/device13measurement12', 13, 12),
('img/device13measurement13', 13, 13),
('img/device13measurement13', 13, 14);
