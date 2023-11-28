
using CvLab.Framework.Common.Configuration;
using CvLab.Framework.Common.Railway.Types;
using CvLab.Framework.Common.ServiceBus;
using CvLab.LazerStockCtrl.Common.Core.Tensor;
using CvLab.LazerStockCtrl.Common.Standard.Core.Tensor;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Rebus.Activation;
using ReWeight.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using static ReWeight.Models.ControlPoint;

namespace ReWeight.Controllers
{
    public class HomeController : Controller
    {
    
        ApplicationContext db;
        public HomeController(ApplicationContext context)
        {
            db = context;
            
            if (db.Regions.Count() == 0)
            {

                Region moskva = new Region { Name = "Москва", IpEmoiv="10.11.22.99" };
                Region spb = new Region { Name = "Питер", IpEmoiv = "10.11.18.99" };
                Region omsk = new Region { Name = "Омск", IpEmoiv = "10.11.19.99" };
                Region chelyabinsk = new Region { Name = "Челябинск", IpEmoiv = "10.11.21.99" };
                Region habr = new Region { Name = "Хабаровск", IpEmoiv = "10.11.20.99" };
                Region taman = new Region { Name = "Тамань", IpEmoiv = "10.11.15.1" };
                Region bataysk = new Region { Name = "Батайск", IpEmoiv = "10.11.17.1" };

                Links linksCp1bataysk = new Links { ConsulDataCapture = "http://10.11.17.1:8500/ui/bataysk/kv/Tensor/DataCapture/visochino_DataCaptureConfigWeight/edit", SGDK = "http://10.11.17.1:8500/ui/bataysk/kv/Tensor/SGDK/Visochino", Nomad = "http://10.11.17.1:4646/ui/jobs/Tensor", Сoefficients = "http://10.11.17.1:8500/ui/bataysk/kv/Tensor/SGDK/Visochino/FuncKoefsA/edit" };
                Links linksCp2bataysk = new Links { ConsulDataCapture = "http://10.11.17.1:8500/ui/bataysk/kv/Tensor/DataCapture/koch_DataCaptureConfig/edit", SGDK = "http://10.11.17.1:8500/ui/bataysk/kv/Tensor/SGDK/Kochevanchic", Nomad = "http://10.11.17.1:4646/ui/jobs/Tensor", Сoefficients = "http://10.11.17.1:8500/ui/bataysk/kv/Tensor/SGDK/Kochevanchic/FuncKoefsA/edit" };

                Links linksCp1chel = new Links { ConsulDataCapture = "http://10.11.21.99:8500/ui/chelyabinsk/kv/Tensor/DataCapture/Cp1_Weight_DataCaptureConfig/edit", SGDK = "http://10.11.21.99:8500/ui/chelyabinsk/kv/Tensor/SGDK/cp1", Nomad = "http://10.11.21.99:4646/ui/jobs/Tensor1", Сoefficients = "http://10.11.21.99:8500/ui/chelyabinsk/kv/Tensor/SGDK/cp1/FuncKoefsA/edit" };
                Links linksCp2chel = new Links { ConsulDataCapture = "http://10.11.21.99:8500/ui/chelyabinsk/kv/Tensor/DataCapture/Cp2_Weight_DataCaptureConfig/edit", SGDK = "http://10.11.21.99:8500/ui/chelyabinsk/kv/Tensor/SGDK/cp2", Nomad = "http://10.11.21.99:4646/ui/jobs/Tensor2", Сoefficients = "http://10.11.21.99:8500/ui/chelyabinsk/kv/Tensor/SGDK/cp2/FuncKoefsA/edit" };
                Links linksCp3chel = new Links { ConsulDataCapture = "http://10.11.21.99:8500/ui/chelyabinsk/kv/Tensor/DataCapture/Cp3_Weight_DataCaptureConfig/edit", SGDK = "http://10.11.21.99:8500/ui/chelyabinsk/kv/Tensor/SGDK/cp3", Nomad = "http://10.11.21.99:4646/ui/jobs/Tensor3", Сoefficients = "http://10.11.21.99:8500/ui/chelyabinsk/kv/Tensor/SGDK/cp3/FuncKoefsA/edit" };
                Links linksCp4chel = new Links { ConsulDataCapture = "http://10.11.21.99:8500/ui/chelyabinsk/kv/Tensor/DataCapture/Cp4_Weight_DataCaptureConfig/edit", SGDK = "http://10.11.21.99:8500/ui/chelyabinsk/kv/Tensor/SGDK/cp4", Nomad = "http://10.11.21.99:4646/ui/jobs/Tensor4", Сoefficients = "http://10.11.21.99:8500/ui/chelyabinsk/kv/Tensor/SGDK/cp4/FuncKoefsA/edit" };
                Links linksCp5chel = new Links { ConsulDataCapture = "http://10.11.21.99:8500/ui/chelyabinsk/kv/Tensor/DataCapture/Cp5_Weight_DataCaptureConfig/edit", SGDK = "http://10.11.21.99:8500/ui/chelyabinsk/kv/Tensor/SGDK/cp5", Nomad = "http://10.11.21.99:4646/ui/jobs/Tensor5", Сoefficients = "http://10.11.21.99:8500/ui/chelyabinsk/kv/Tensor/SGDK/cp5/FuncKoefsA/edit" };

                Links linksCp1habr = new Links { ConsulDataCapture = "http://10.11.20.99:8500/ui/habr/kv/Tensor/DataCapture/Cp1_Weight_DataCaptureConfig/edit", SGDK = "http://10.11.20.99:8500/ui/habr/kv/Tensor/SGDK/cp1", Nomad = "http://10.11.20.99:4646/ui/jobs/Tensor1", Сoefficients = "http://10.11.20.99:8500/ui/habr/kv/Tensor/SGDK/cp1/FuncKoefsA/edit" };
                Links linksCp2habr = new Links { ConsulDataCapture = "http://10.11.20.99:8500/ui/habr/kv/Tensor/DataCapture/Cp2_Weight_DataCaptureConfig/edit", SGDK = "http://10.11.20.99:8500/ui/habr/kv/Tensor/SGDK/cp2", Nomad = "http://10.11.20.99:4646/ui/jobs/Tensor2", Сoefficients = "http://10.11.20.99:8500/ui/habr/kv/Tensor/SGDK/cp2/FuncKoefsA/edit" };

                Links linksCp1taman = new Links { ConsulDataCapture = "http://10.11.15.1:8500/ui/taman/kv/Tensor/DataCapture/Taman1_Weight_DataCaptureConfig/edit", SGDK = "http://10.11.15.1:8500/ui/taman/kv/Tensor/SGDK/Taman1", Nomad = "http://10.11.15.1:4646/ui/jobs/Tensor1", Сoefficients = "http://10.11.15.1:8500/ui/taman/kv/Tensor/SGDK/Taman1/FuncKoefsA/edit" };
                Links linksCp2taman = new Links { ConsulDataCapture = "http://10.11.15.1:8500/ui/taman/kv/Tensor/DataCapture/Taman2_Weight_DataCaptureConfig/edit", SGDK = "http://10.11.15.1:8500/ui/taman/kv/Tensor/SGDK/Taman2", Nomad = "http://10.11.15.1:4646/ui/jobs/Tensor2", Сoefficients = "http://10.11.15.1:8500/ui/taman/kv/Tensor/SGDK/Taman2/FuncKoefsA/edit" };
                Links linksCp3taman = new Links { ConsulDataCapture = "http://10.11.15.1:8500/ui/taman/kv/Tensor/DataCapture/Taman3_Weight_DataCaptureConfig/edit", SGDK = "http://10.11.15.1:8500/ui/taman/kv/Tensor/SGDK/Taman3", Nomad = "http://10.11.15.1:4646/ui/jobs/Tensor3", Сoefficients = "http://10.11.15.1:8500/ui/taman/kv/Tensor/SGDK/Taman3/FuncKoefsA/edit" };
                Links linksCp4taman = new Links { ConsulDataCapture = "http://10.11.15.1:8500/ui/taman/kv/Tensor/DataCapture/Taman4_Weight_DataCaptureConfig/edit", SGDK = "http://10.11.15.1:8500/ui/taman/kv/Tensor/SGDK/Taman4", Nomad = "http://10.11.15.1:4646/ui/jobs/Tensor4", Сoefficients = "http://10.11.15.1:8500/ui/taman/kv/Tensor/SGDK/Taman4/FuncKoefsA/edit" };


                Links linksCp1mos = new Links { ConsulDataCapture = "http://10.11.22.99:8500/ui/oreh/kv/Tensor/DataCapture/Cp1_Weight_DataCaptureConfig/edit", SGDK = "http://10.11.22.99:8500/ui/oreh/kv/Tensor/SGDK/cp1", Nomad = "http://10.11.22.99:4646/ui/jobs/Tensor1", Сoefficients = "http://10.11.22.99:8500/ui/oreh/kv/Tensor/SGDK/cp1/FuncKoefsA/edit" };
                Links linksCp2mos = new Links { ConsulDataCapture = "http://10.11.22.99:8500/ui/oreh/kv/Tensor/DataCapture/Cp2_Weight_DataCaptureConfig/edit", SGDK = "http://10.11.22.99:8500/ui/oreh/kv/Tensor/SGDK/cp2", Nomad = "http://10.11.22.99:4646/ui/jobs/Tensor2", Сoefficients = "http://10.11.22.99:8500/ui/oreh/kv/Tensor/SGDK/cp2/FuncKoefsA/edit" };
                Links linksCp3mos = new Links { ConsulDataCapture = "http://10.11.22.99:8500/ui/oreh/kv/Tensor/DataCapture/Cp3_Weight_DataCaptureConfig/edit", SGDK = "http://10.11.22.99:8500/ui/oreh/kv/Tensor/SGDK/cp3", Nomad = "http://10.11.22.99:4646/ui/jobs/Tensor3", Сoefficients = "http://10.11.22.99:8500/ui/oreh/kv/Tensor/SGDK/cp3/FuncKoefsA/edit" };
                Links linksCp4mos = new Links { ConsulDataCapture = "http://10.11.22.99:8500/ui/oreh/kv/Tensor/DataCapture/Cp4_Weight_DataCaptureConfig/edit", SGDK = "http://10.11.22.99:8500/ui/oreh/kv/Tensor/SGDK/cp4", Nomad = "http://10.11.22.99:4646/ui/jobs/Tensor4", Сoefficients = "http://10.11.22.99:8500/ui/oreh/kv/Tensor/SGDK/cp4/FuncKoefsA/edit" };

                Links linksCp1spb = new Links { ConsulDataCapture = "http://10.11.18.99:8500/ui/spb/kv/Tensor/DataCapture/Cp1_Weight_DataCaptureConfig/edit", SGDK = "http://10.11.18.99:8500/ui/spb/kv/Tensor/SGDK/cp1", Nomad = "http://10.11.18.99:4646/ui/jobs/Tensor1", Сoefficients = "http://10.11.18.99:8500/ui/spb/kv/Tensor/SGDK/cp1/FuncKoefsA/edit" };
                Links linksCp2spb = new Links { ConsulDataCapture = "http://10.11.18.99:8500/ui/spb/kv/Tensor/DataCapture/Cp2_Weight_DataCaptureConfig/edit", SGDK = "http://10.11.18.99:8500/ui/spb/kv/Tensor/SGDK/cp2", Nomad = "http://10.11.18.99:4646/ui/jobs/Tensor2", Сoefficients = "http://10.11.18.99:8500/ui/spb/kv/Tensor/SGDK/cp2/FuncKoefsA/edit" };

                Links linksCp1omsk = new Links { ConsulDataCapture = "http://10.11.19.99:8500/ui/vhodnaya/kv/Tensor/DataCapture/Cp1_Weight_DataCaptureConfig/edit", SGDK = "http://10.11.19.99:8500/ui/vhodnaya/kv/Tensor/SGDK/cp1", Nomad = "http://10.11.19.99:4646/ui/jobs/Tensor1", Сoefficients = "http://10.11.19.99:8500/ui/vhodnaya/kv/Tensor/SGDK/cp1/FuncKoefsA/edit" };
                Links linksCp2omsk = new Links { ConsulDataCapture = "http://10.11.19.99:8500/ui/vhodnaya/kv/Tensor/DataCapture/Cp2_Weight_DataCaptureConfig/edit", SGDK = "http://10.11.19.99:8500/ui/vhodnaya/kv/Tensor/SGDK/cp2", Nomad = "http://10.11.19.99:4646/ui/jobs/Tensor2", Сoefficients = "http://10.11.19.99:8500/ui/vhodnaya/kv/Tensor/SGDK/cp2/FuncKoefsA/edit" };
                Links linksCp3omsk = new Links { ConsulDataCapture = "http://10.11.19.99:8500/ui/vhodnaya/kv/Tensor/DataCapture/Cp3_Weight_DataCaptureConfig/edit", SGDK = "http://10.11.19.99:8500/ui/vhodnaya/kv/Tensor/SGDK/cp3", Nomad = "http://10.11.19.99:4646/ui/jobs/Tensor3", Сoefficients = "http://10.11.19.99:8500/ui/vhodnaya/kv/Tensor/SGDK/cp3/FuncKoefsA/edit" };
                Links linksCp4omsk = new Links { ConsulDataCapture = "http://10.11.19.99:8500/ui/vhodnaya/kv/Tensor/DataCapture/Cp4_Weight_DataCaptureConfig/edit", SGDK = "http://10.11.19.99:8500/ui/vhodnaya/kv/Tensor/SGDK/cp4", Nomad = "http://10.11.19.99:4646/ui/jobs/Tensor4", Сoefficients = "http://10.11.19.99:8500/ui/vhodnaya/kv/Tensor/SGDK/cp4/FuncKoefsA/edit" };


                ControlPoint cp1bataysk = new ControlPoint { ControlPointId = 1, Name = "Староминская", Iplokal = "192.168.254.85", IpVpn = "10.11.17.72", IPMI = "192.168.255.85", RegionId = bataysk.Id, Region = bataysk, LinksId = linksCp1bataysk.Id, Links = linksCp1bataysk, Information = "дополнительная информация" };
                ControlPoint cp2bataysk = new ControlPoint { ControlPointId = 2, Name = "Тихорецкая", Iplokal = "192.168.254.75", IpVpn = "10.11.17.41", IPMI = "192.168.255.75", RegionId = bataysk.Id, Region = bataysk, LinksId = linksCp2bataysk.Id, Links = linksCp2bataysk, Information = "дополнительная информация" };

                ControlPoint cp1habr = new ControlPoint { ControlPointId = 1, Name = "Корфовская", Iplokal = "192.168.254.41", IpVpn = "10.11.20.41", IPMI = "192.168.255.41", RegionId = habr.Id, Region = habr, LinksId = linksCp1habr.Id, Links = linksCp1habr, Information = "дополнительная информация" };
                ControlPoint cp2habr = new ControlPoint { ControlPointId = 2, Name = "Покровский", Iplokal = "192.168.254.42", IpVpn = "10.11.20.42", IPMI = "192.168.255.42", RegionId = habr.Id, Region = habr, LinksId = linksCp2habr.Id, Links = linksCp2habr, Information = "дополнительная информация" };

                ControlPoint cp1taman = new ControlPoint { ControlPointId = 1, Name = "Вышестеблиевская-1", Iplokal = "192.168.254.41", IpVpn = "10.11.15.41", IPMI = "192.168.255.41", RegionId = taman.Id, Region = taman, LinksId = linksCp1taman.Id, Links = linksCp1taman, Information = "дополнительная информация" };
                ControlPoint cp2taman = new ControlPoint { ControlPointId = 2, Name = "Вышестеблиевская-2", Iplokal = "192.168.254.42", IpVpn = "10.11.15.42", IPMI = "192.168.255.42", RegionId = taman.Id, Region = taman, LinksId = linksCp1taman.Id, Links = linksCp1taman, Information = "дополнительная информация" };
                ControlPoint cp3taman = new ControlPoint { ControlPointId = 3, Name = "Тамань-1", Iplokal = "192.168.254.43", IpVpn = "10.11.15.43", IPMI = "192.168.255.43", RegionId = taman.Id, Region = taman, LinksId = linksCp1taman.Id, Links = linksCp1taman, Information = "дополнительная информация" };
                ControlPoint cp4taman = new ControlPoint { ControlPointId = 4, Name = "Тамань-2", Iplokal = "192.168.254.44", IpVpn = "10.11.15.44", IPMI = "192.168.255.44", RegionId = taman.Id, Region = taman, LinksId = linksCp1taman.Id, Links = linksCp1taman, Information = "дополнительная информация" };



                ControlPoint cp1cpchelyabinsk = new ControlPoint { ControlPointId = 1, Name = "Бишкиль", Iplokal = "192.168.254.41", IpVpn = "10.11.21.41", IPMI = "192.168.255.41", RegionId = chelyabinsk.Id, Region = chelyabinsk, LinksId = linksCp1chel.Id, Links = linksCp1chel, Information = "дополнительная информация" };
                ControlPoint cp2cpchelyabinsk = new ControlPoint { ControlPointId = 2, Name = "Еманжелинск", Iplokal = "192.168.254.42", IpVpn = "10.11.21.42", IPMI = "192.168.255.41", RegionId = chelyabinsk.Id, Region = chelyabinsk, LinksId = linksCp2chel.Id, Links = linksCp2chel, Information = "дополнительная информация" };
                ControlPoint cp3cpchelyabinsk = new ControlPoint { ControlPointId = 3, Name = "Потанино", Iplokal = "192.168.254.43", IpVpn = "10.11.21.43", IPMI = "192.168.255.41", RegionId = chelyabinsk.Id, Region = chelyabinsk, LinksId = linksCp3chel.Id, Links = linksCp3chel, Information = "дополнительная информация" };
                ControlPoint cp4cpchelyabinsk = new ControlPoint { ControlPointId = 4, Name = "Баландино", Iplokal = "192.168.254.44", IpVpn = "10.11.21.44", IPMI = "192.168.255.41", RegionId = chelyabinsk.Id, Region = chelyabinsk, LinksId = linksCp4chel.Id, Links = linksCp4chel, Information = "дополнительная информация" };
                ControlPoint cp5cpchelyabinsk = new ControlPoint { ControlPointId = 5, Name = "Аргояш", Iplokal = "192.168.254.45", IpVpn = "10.11.21.45", IPMI = "192.168.255.41", RegionId = chelyabinsk.Id, Region = chelyabinsk, LinksId = linksCp5chel.Id, Links = linksCp5chel, Information = "дополнительная информация" };

                ControlPoint cp1mos = new ControlPoint { ControlPointId = 1, Name = "Усад", Iplokal = "192.168.254.41", IpVpn = "10.11.22.41", IPMI = "192.168.255.41", RegionId = moskva.Id, Region = moskva, LinksId = linksCp1mos.Id, Links = linksCp1mos, Information = "дополнительная информация" };
                ControlPoint cp2mos = new ControlPoint { ControlPointId = 2, Name = "Давыдово", Iplokal = "192.168.254.42", IpVpn = "10.11.22.42", IPMI = "192.168.255.42", RegionId = moskva.Id, Region = moskva, LinksId = linksCp2mos.Id, Links = linksCp2mos, Information = "дополнительная информация" };
                ControlPoint cp3mos = new ControlPoint { ControlPointId = 3, Name = "Дрезна", Iplokal = "192.168.254.43", IpVpn = "10.11.22.43", IPMI = "192.168.255.43", RegionId = moskva.Id, Region = moskva, LinksId = linksCp3mos.Id, Links = linksCp3mos, Information = "дополнительная информация" };
                ControlPoint cp4mos = new ControlPoint { ControlPointId = 4, Name = "Поточино", Iplokal = "192.168.254.44", IpVpn = "10.11.22.44", IPMI = "192.168.255.44", RegionId = moskva.Id, Region = moskva, LinksId = linksCp4mos.Id, Links = linksCp4mos, Information = "дополнительная информация" };

                ControlPoint cp1spb = new ControlPoint { ControlPointId = 1, Name = "Ижоры", Iplokal = "192.168.254.41", IpVpn = "10.11.18.41", IPMI = "192.168.255.41", RegionId = spb.Id, Region = spb, LinksId = linksCp1spb.Id, Links = linksCp1spb, Information = "дополнительная информация" };
                ControlPoint cp2spb = new ControlPoint { ControlPointId = 2, Name = "Дача Долгорукова", Iplokal = "192.168.254.42", IpVpn = "10.11.18.42", IPMI = "192.168.255.42", RegionId = spb.Id, Region = spb, LinksId = linksCp2spb.Id, Links = linksCp2spb, Information = "дополнительная информация" };

                ControlPoint cp1omsk = new ControlPoint { ControlPointId = 1, Name = "Густафьево", Iplokal = "192.168.254.41", IpVpn = "10.11.19.41", IPMI = "192.168.255.41", RegionId = omsk.Id, Region = omsk, LinksId = linksCp1omsk.Id, Links = linksCp1omsk, Information = "дополнительная информация" };
                ControlPoint cp2omsk = new ControlPoint { ControlPointId = 2, Name = "Жатва", Iplokal = "192.168.254.42", IpVpn = "10.11.19.42", IPMI = "192.168.255.42", RegionId = omsk.Id, Region = omsk, LinksId = linksCp2omsk.Id, Links = linksCp2omsk, Information = "дополнительная информация" };
                ControlPoint cp3omsk = new ControlPoint { ControlPointId = 3, Name = "Новокиевский", Iplokal = "192.168.254.43", IpVpn = "10.11.19.43", IPMI = "192.168.255.43", RegionId = omsk.Id, Region = omsk, LinksId = linksCp3omsk.Id, Links = linksCp3omsk, Information = "дополнительная информация" };
                ControlPoint cp4omsk = new ControlPoint { ControlPointId = 4, Name = "Мариановка", Iplokal = "192.168.254.44", IpVpn = "10.11.19.43", IPMI = "192.168.255.43", RegionId = omsk.Id, Region = omsk, LinksId = linksCp4omsk.Id, Links = linksCp4omsk, Information = "дополнительная информация" };



                db.Regions.AddRange(moskva, spb, omsk, chelyabinsk, habr, taman, bataysk);
                db.Links.AddRange(linksCp1mos, linksCp2mos, linksCp3mos, linksCp4mos, linksCp1spb, linksCp2spb, linksCp1omsk, linksCp2omsk, linksCp3omsk, linksCp4omsk,
                    linksCp1chel, linksCp2chel, linksCp3chel, linksCp4chel, linksCp5chel, linksCp1habr, linksCp2habr, linksCp1taman, linksCp2taman, linksCp3taman, linksCp4taman,
                    linksCp1bataysk, linksCp2bataysk);
                db.controlPoints.AddRange(cp1mos, cp2mos, cp3mos, cp4mos, cp1spb, cp2spb, cp1omsk, cp2omsk, cp3omsk, cp4omsk, cp1cpchelyabinsk, cp2cpchelyabinsk,
                    cp3cpchelyabinsk, cp4cpchelyabinsk, cp5cpchelyabinsk, cp1habr, cp2habr, cp1taman, cp2taman, cp3taman, cp4taman, cp1bataysk, cp2bataysk);
                db.SaveChanges();


            }
        }

        public async Task<IActionResult> Index(SortState sortOrder = SortState.RegionAsc)
        {

            IQueryable<ControlPoint> controlPoints = db.controlPoints.Include(x => x.Region);

            ViewData["RegionSort"] = sortOrder == SortState.RegionAsc ? SortState.RegionDesc : SortState.RegionAsc;

            controlPoints = sortOrder switch
            {

                SortState.RegionAsc => controlPoints.OrderBy(s => s.Region.Name),
                SortState.RegionDesc => controlPoints.OrderByDescending(s => s.Region.Name),
                _ => controlPoints.OrderBy(s => s.Name),
            };
            return View(await controlPoints.AsNoTracking().ToListAsync());

        }




        public async Task<IActionResult> Details(int? id)
        {
            if (id != null)
            {
                ControlPoint controlPoint = await db.controlPoints.FirstOrDefaultAsync(p => p.Id == id);
                Region region = await db.Regions.FirstOrDefaultAsync(p => p.Id == controlPoint.RegionId);
                Links links = await db.Links.FirstOrDefaultAsync(p => p.Id == controlPoint.LinksId);
                if (controlPoint != null)
                    return View(controlPoint);
            }
            return NotFound();
        }
        public async Task<IActionResult> Edit(int? id)
        {
            if (id != null)
            {
                ControlPoint controlPoint = await db.controlPoints.FirstOrDefaultAsync(p => p.Id == id);
                Region region = await db.Regions.FirstOrDefaultAsync(p => p.Id == controlPoint.RegionId);
                Links link = await db.Links.FirstOrDefaultAsync(p => p.Id == controlPoint.LinksId);
                if (controlPoint != null)
                    return View(controlPoint);
            }
            return NotFound();
        }
        [HttpPost]
        public async Task<IActionResult> Edit(ControlPoint controlPoint, Region region, Links links)
        {
            //ControlPoint controlPointTemp = await db.controlPoints.FirstOrDefaultAsync(p => p.Id == controlPoint.Id);

            int countRegion = await db.controlPoints.CountAsync(p => p.Region.Name == region.Name);
            int countLinks = await db.controlPoints.CountAsync(p => p.Links.Nomad == links.Nomad || p.Links.SGDK == links.SGDK || p.Links.Сoefficients == links.Сoefficients);

            if (countRegion != 0) {
                Region regionflag = await db.Regions.FirstOrDefaultAsync(p => p.Name == region.Name);
                controlPoint.RegionId = regionflag.Id;
            }


            if (countLinks != 0)
            {
                Links linksflag = await db.Links.FirstOrDefaultAsync(p => p.Nomad == links.Nomad || p.SGDK == links.SGDK || p.Сoefficients == links.Сoefficients);
                linksflag.Nomad = links.Nomad;
                links.SGDK = links.SGDK;
                links.Сoefficients = links.Сoefficients;
                controlPoint.LinksId = linksflag.Id;
            }


            db.controlPoints.Update(controlPoint);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(ControlPoint controlPoint, Region region)
        {
            Region regionflag = await db.Regions.FirstOrDefaultAsync(p => p.Name == region.Name);
            if (regionflag == null)
            {

                db.controlPoints.AddRange(controlPoint);
                //db.Regions.AddRange(region);
            }
            else
            {
                // controlPoint.Region = region;
                controlPoint.RegionId = regionflag.Id;
                db.controlPoints.Add(controlPoint);

            }
            await db.SaveChangesAsync();
            return RedirectToAction("Index");

        }
        [HttpGet]
        [ActionName("Delete")]
        public async Task<IActionResult> ConfirmDelete(int? id)
        {
            if (id != null)
            {
                ControlPoint controlPoint = await db.controlPoints.FirstOrDefaultAsync(p => p.Id == id);
                if (controlPoint != null)
                    return View(controlPoint);
            }
            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id != null)
            {
                ControlPoint controlPoint = await db.controlPoints.FirstOrDefaultAsync(p => p.Id == id);
                Region region = await db.Regions.FirstOrDefaultAsync(p => p.Id == controlPoint.RegionId);
                Links link = await db.Links.FirstOrDefaultAsync(p => p.Id == controlPoint.LinksId);
                int count = await db.controlPoints.CountAsync(p => p.RegionId == region.Id);
                if (controlPoint != null && count == 1)
                {
                    db.Regions.Remove(region);
                    db.controlPoints.Remove(controlPoint);

                }
                if (controlPoint != null && count != 1)
                {

                    db.controlPoints.Remove(controlPoint);
                    //await db.SaveChangesAsync();
                    //return RedirectToAction("Index");
                }
                db.Links.Remove(link);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");

            }
            return NotFound();
        }

        public async Task<IActionResult> InterReWeightAsync(int? id)
        {
            if (id != null)
            {
                ControlPoint controlPoint = await db.controlPoints.FirstOrDefaultAsync(p => p.Id == id);
                Links links = await db.Links.FirstOrDefaultAsync(p => p.Id == controlPoint.LinksId);
                Region region = await db.Regions.FirstOrDefaultAsync(p => p.Id == controlPoint.RegionId);
                if (controlPoint != null)
                    return View(controlPoint);
            }
            return NotFound();
        }

        public async Task<IActionResult> GetOsc(int? id)
        {
            if (id != null)
            {
                ControlPoint controlPoint = await db.controlPoints.FirstOrDefaultAsync(p => p.Id == id);
                Links links = await db.Links.FirstOrDefaultAsync(p => p.Id == controlPoint.LinksId);
                Region region = await db.Regions.FirstOrDefaultAsync(p => p.Id == controlPoint.RegionId);
                if (controlPoint != null)
                    return View(controlPoint);
            }
            return NotFound();
        }


        [HttpPost]
        public async Task<IActionResult> InterReWeightAsync(ControlPoint controlPoint, Region region)
        {
            //Region TempRegion = await db.Regions.FirstOrDefaultAsync(p => p.Id == region.Id);
            ControlPoint TempControlPoint = await db.controlPoints.FirstOrDefaultAsync(p => p.Id == controlPoint.Id);
            Region TempRegion = await db.Regions.FirstOrDefaultAsync(p => p.Id == TempControlPoint.RegionId);
            TempControlPoint.Train = controlPoint.Train;

            var someContainerAdapter = new BuiltinHandlerActivator();
            var cfg = new ServiceBusConfig { RabbitMqConnectionString = $"amqp://user:12345@{TempRegion.IpEmoiv}:5672/" };
            var bus = someContainerAdapter.DeployOneWayMessageBus(cfg);

            var trainDirection = TempControlPoint.Train.Direction == 1 ? TrainDirection.Forward : TrainDirection.Reverse;

            int trainId = TempControlPoint.Train.TrainId;

            await bus.Publish(new MsgTensorDatasetReady<TensorServiceTypeWeight>
            {
                TrainId = trainId,
                ControlPointId = TempControlPoint.ControlPointId,
                DatasetEndpointAddress = $"http://{TempControlPoint.IpVpn}:5155/api/DataCapture/GetTensorDataset?TrainId={trainId}&isPhantom=false",
                Direction = trainDirection,
                BrokenSensorsEndpointAddress = $"http://{TempControlPoint.IpVpn}:5155/api/DataCapture/GetDatasetBrokenSensors?trainId={trainId}&isPhantom=false"
            });

            return RedirectToAction("InterReWeight");

        }

    }
}
